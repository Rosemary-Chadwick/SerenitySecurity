import React, { useState, useEffect } from 'react';
import { Row, Col, Card, CardBody, CardTitle, CardHeader } from 'reactstrap';
import { PieChart, Pie, Cell, BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { useTheme } from '../theme/ThemeContext';
import { getVulnerabilitySeverityStats, getAssetVulnerabilityStats } from '../../managers/dashboardManager';

export const SeverityDistributionChart = ({ data }) => {
  const COLORS = ['#ff3333', '#a83246', '#d9b55a', '#5a9178', '#808080'];
  const total = data.reduce((sum, item) => sum + item.value, 0);
  const { colors } = useTheme();

  const renderCustomLegend = (props) => {
    const { payload } = props;
    
    return (
      <ul className="chart-legend" style={{
        listStyle: 'none',
        padding: 0,
        margin: 0,
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center'
      }}>
        {payload.map((entry, index) => (
          <li key={`item-${index}`} style={{
            display: 'flex',
            alignItems: 'center',
            marginRight: '12px',
            marginBottom: '6px'
          }}>
            <div style={{
              backgroundColor: entry.color,
              width: '10px',
              height: '10px',
              borderRadius: '50%',
              marginRight: '5px'
            }}/>
            <span>{entry.value}</span>
          </li>
        ))}
      </ul>
    );
  };
  
  return (
    <div className="dashboard-chart-container">
      <div style={{ width: '100%', height: '100%', position: 'relative' }}>
        <ResponsiveContainer>
          <PieChart>
            <Pie
              data={data}
              cx="50%"
              cy="50%"
              innerRadius={60}
              outerRadius={80}
              paddingAngle={2}
              dataKey="value"
            >
              {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
              ))}
            </Pie>
            <Tooltip 
              formatter={(value, name) => [`${value} (${((value / total) * 100).toFixed(1)}%)`, name]}
              contentStyle={{ 
                backgroundColor: '#fdf6e3',
                color: '#1b2a3c',
                borderColor: '#1b2a3c',
                zIndex: 1000
              }}
            />
            <Legend content={renderCustomLegend} />
          </PieChart>
        </ResponsiveContainer>
        
        {/* Total counter in the middle */}
        <div
          style={{
            position: 'absolute',
            top: '45%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            textAlign: 'center',
            pointerEvents: 'none'
          }}
        >
          <div style={{ fontSize: '24px', fontWeight: 'bold' }}>{total}</div>
        </div>
      </div>
    </div>
  );
};

/* Removed Remediation Status Chart Component */

// Component for Asset Vulnerability Comparison
export const AssetVulnerabilityComparisonChart = ({ data }) => {
  const { colors } = useTheme();

  const renderCustomLegend = (props) => {
    const { payload } = props;

    return (
      <ul className="chart-legend" style={{
        listStyle: 'none',
        padding: 0,
        margin: 0,
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center'
      }}>
        {payload.map((entry, index) => (
          <li key={`item-${index}`} style={{
            display: 'flex',
            alignItems: 'center',
            marginRight: '12px',
            marginBottom: '6px'
          }}>
            <div style={{
              backgroundColor: entry.color,
              width: '10px',
              height: '10px',
              borderRadius: '50%',
              marginRight: '5px'
            }}/>
            <span>{entry.value}</span>
          </li>
        ))}
      </ul>
    );
  };
  
  return (
    <div className="dashboard-chart-container">
      <ResponsiveContainer width="100%" height="100%">
        <BarChart
          data={data}
          margin={{ top: 20, right: 50, left: 20, bottom: 5 }}
        >
          <CartesianGrid strokeDasharray="3 3" stroke={colors.isDarkMode ? '#555' : '#ddd'} />
          <XAxis dataKey="name" tick={{ fill: colors.cardText }} />
          <YAxis tick={{ fill: colors.cardText }} />
          <Tooltip 
            contentStyle={{ 
              backgroundColor: '#fdf6e3',
              color: '#1b2a3c',
              borderColor: '#1b2a3c'
            }}
          />
          <Legend />
          <Bar dataKey="Critical" stackId="a" fill="#ff3333" />
          <Bar dataKey="High" stackId="a" fill="#a83246" />
          <Bar dataKey="Medium" stackId="a" fill="#d9b55a" />
          <Bar dataKey="Low" stackId="a" fill="#5a9178" />
          <Bar dataKey="Unknown" stackId="a" fill="#808080" />
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
};

// Main Dashboard component
const DashboardCharts = () => {
  const { colors } = useTheme();
  
  // Use state to manage data
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [severityData, setSeverityData] = useState([]);
  // Remediation data state removed
  const [assetComparisonData, setAssetComparisonData] = useState([]);
  
  useEffect(() => {
    setLoading(true);
    setError(null);
    
    Promise.all([
      getVulnerabilitySeverityStats(),
      // getRemediationStatusStats(), - removed
      getAssetVulnerabilityStats()
    ])
    .then(([severityStats, assetStats]) => {
      console.log("Severity stats:", severityStats);
      console.log("Asset stats:", assetStats);
      
      setSeverityData(severityStats);
      setAssetComparisonData(assetStats);
      setLoading(false);
    })
    .catch(err => {
      console.error("Error loading dashboard data:", err);
      setError(err.message);
      setLoading(false);
    });
  }, []);
  
  if (loading) {
    return (
      <Card className="mb-4">
        <CardHeader style={{ backgroundColor: colors.primary, color: colors.secondary }}>
          <CardTitle tag="h5" className="mb-0">Security Metrics For All Personal Assets</CardTitle>
        </CardHeader>
        <CardBody style={{ backgroundColor: colors.cardBg, color: colors.cardText }} className="text-center">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading charts...</span>
          </div>
          <p className="mt-3">Loading security metrics...</p>
        </CardBody>
      </Card>
    );
  }
  
  if (error) {
    return (
      <Card className="mb-4">
        <CardHeader style={{ backgroundColor: colors.primary, color: colors.secondary }}>
          <CardTitle tag="h5" className="mb-0">Security Metrics For All Personal Assets</CardTitle>
        </CardHeader>
        <CardBody style={{ backgroundColor: colors.cardBg, color: colors.cardText }}>
          <div className="alert alert-danger">
            Error loading metrics: {error}
          </div>
        </CardBody>
      </Card>
    );
  }
  
  return (
    <Card className="mb-4">
      <CardHeader style={{ backgroundColor: colors.primary, color: colors.secondary }}>
        <CardTitle tag="h5" className="mb-0">Security Metrics For All Personal Assets</CardTitle>
      </CardHeader>
      <CardBody style={{ backgroundColor: colors.cardBg, color: colors.cardText }}>
        <h6 className="text-center mb-3">Vulnerability Severity Distribution</h6>
        <SeverityDistributionChart data={severityData} />
        
        {/* Remediation Status Chart section removed */}
        
        <h6 className="text-center mb-3 mt-4">Asset Vulnerability Comparison</h6>
        <AssetVulnerabilityComparisonChart data={assetComparisonData} />
      </CardBody>
    </Card>
  );
};

export default DashboardCharts;