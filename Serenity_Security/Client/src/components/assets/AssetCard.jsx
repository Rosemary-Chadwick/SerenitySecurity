import React from 'react';
import { Link } from 'react-router-dom';
import { Card, CardBody, CardTitle, Button, CardHeader } from 'reactstrap';
import { useTheme } from '../theme/ThemeContext';
import VulnerabilityDonutChart from '../charts/VulnerabilityDonutChart';

const AssetCard = ({ asset }) => {
  const { colors } = useTheme();
  
  // Mock data for the chart (replace with actual data from your API)
  const vulnerabilityData = [
    { name: 'High', value: asset.highVulnerabilities || 2, color: '#dc3545' },
    { name: 'Medium', value: asset.mediumVulnerabilities || 4, color: '#ffc107' },
    { name: 'Low', value: asset.lowVulnerabilities || 7, color: '#28a745' }
  ];
  
  const totalVulnerabilities = vulnerabilityData.reduce((sum, item) => sum + item.value, 0);
  
  return (
    <Link 
    to={`/assets/${asset.id}`} 
    className="asset-card-link"
    style={{ textDecoration: 'none' }}
  >
    <Card className="asset-card h-100">
        <CardHeader style={{ backgroundColor: colors.primary,
           color: colors.secondary,
           borderTopLeftRadius: 'inherit',
      borderTopRightRadius: 'inherit',
      padding: '0.75rem 1rem',
      marginBottom: 0,
      width: '100%'
            }}>
        <CardTitle tag="h5" className="mb-0 text-center">
          {asset.systemName}
        </CardTitle>
        </CardHeader>

        <CardBody style={{ 
        backgroundColor: colors.cardBg,
        color: colors.cardText
      }}>
        {/* <div className="mb-3">
          <div className="text-center mb-2" style={{ height: '130px' }}>
            <VulnerabilityDonutChart 
              data={vulnerabilityData} 
              total={totalVulnerabilities}
              width={120}
              height={120}
            />
          </div> */}
          
          <div className="d-flex">
    {/* Left side - Asset info */}
    <div className="flex-grow-1 me-2">
      <div className="mb-3">
        <strong>IP:</strong> {asset.ipAddress}
      </div>
      <div className="mb-3">
        <strong>Type:</strong> {asset.systemTypeName}
      </div>
      <div className="mb-3">
        <strong>OS:</strong> {asset.osVersion}
      </div>
      <div className="mb-3">
        <strong>Status:</strong>{' '}
        <span className={`status-badge ${asset.isActive ? 'status-fixed' : 'status-high'}`}>
          {asset.isActive ? 'Active' : 'Inactive'}
        </span>
      </div>
    </div>

    <div className="ms-2 d-flex align-items-center" style={{ marginTop: '15px' }}>
      <VulnerabilityDonutChart 
        data={vulnerabilityData} 
        total={totalVulnerabilities}
        width={120}
        height={155}
      />
    </div>
  </div>
        
        <div className="d-grid gap-2">
          {/* <Button
            tag={Link}
            to={`/assets/${asset.id}`}
            style={{
              backgroundColor: colors.buttonHighlight,
              borderColor: colors.buttonHighlight,
              color: colors.primary
            }}
            size="sm"
          >
            View Details
          </Button> */}
          {/* <Button
            tag={Link}
            to={`/report/${asset.latestReportId || 0}`}
            color="secondary"
            outline
            size="sm"
            disabled={!asset.latestReportId}
          >
            Latest Report
          </Button> */}
        </div>
      </CardBody>
    </Card>
    </Link>
  );
};

export default AssetCard;