import React from 'react';
import { Row, Col, Card, CardBody, CardTitle, CardHeader } from 'reactstrap';
import { PieChart, Pie, Cell, BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { useTheme } from '../theme/ThemeContext';

// Component for Vulnerability Severity Distribution
export const SeverityDistributionChart = ({ data }) => {
  // Muted color palette - you can easily modify these colors
  const COLORS = ['#a83246', '#d9b55a', '#5a9178'];
  const total = data.reduce((sum, item) => sum + item.value, 0);
  const { colors } = useTheme();
  
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
            <Legend />
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

// Component for Remediation Status Chart
export const RemediationStatusChart = ({ data }) => {
  // Muted color palette - you can easily modify these colors
  const COLORS = ['#5c95a8', '#a83246'];
  const total = data.reduce((sum, item) => sum + item.value, 0);
  const { colors } = useTheme();
  
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
                borderColor: '#1b2a3c'
              }}
            />
            <Legend />
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

// Component for Asset Vulnerability Comparison
export const AssetVulnerabilityComparisonChart = ({ data }) => {
  // Muted color palette - you can easily modify these colors
  const { colors } = useTheme();
  
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
          <Bar dataKey="high" stackId="a" fill="#a83246" />
          <Bar dataKey="medium" stackId="a" fill="#d9b55a" />
          <Bar dataKey="low" stackId="a" fill="#5a9178" />
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
};

// Main Dashboard component combining all charts
const DashboardCharts = () => {
  const { colors } = useTheme();
  
  // Mock data - replace with actual API data
  const severityData = [
    { name: 'High', value: 12 },
    { name: 'Medium', value: 24 },
    { name: 'Low', value: 18 }
  ];
  
  const remediationData = [
    { name: 'Fixed', value: 28 },
    { name: 'Pending', value: 26 }
  ];
  
  const assetComparisonData = [
    { name: 'Web Server', high: 5, medium: 8, low: 4 },
    { name: 'Database', high: 3, medium: 6, low: 5 },
    { name: 'Firewall', high: 1, medium: 3, low: 7 },
    { name: 'File Server', high: 3, medium: 7, low: 2 }
  ];
  
  return (
    <Card className="mb-4">
      <CardHeader style={{ backgroundColor: colors.primary, color: colors.secondary }}>
        <CardTitle tag="h5" className="mb-0">Security Metrics For All Personal Assets</CardTitle>
      </CardHeader>
      <CardBody style={{ backgroundColor: colors.cardBg, color: colors.cardText }}>
        <SeverityDistributionChart data={severityData} />
        <RemediationStatusChart data={remediationData} />
        <AssetVulnerabilityComparisonChart data={assetComparisonData} />
      </CardBody>
    </Card>
  );
};

export default DashboardCharts;
