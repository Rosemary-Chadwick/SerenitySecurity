import React from 'react';
import { Link } from 'react-router-dom';
import { Card, CardBody, CardTitle, Button, CardHeader } from 'reactstrap';
import { useTheme } from '../theme/ThemeContext';
import VulnerabilityDonutChart from '../charts/VulnerabilityDonutChart';

const AssetCard = ({ asset, vulnerabilityData }) => {
  const { colors } = useTheme();

  console.log("Asset:", asset.systemName);
  console.log("Vulnerability data for this asset:", vulnerabilityData);
  
  const chartData = [
    { name: 'Critical', value: vulnerabilityData?.critical || 0, color: '#ff3333' },
    { name: 'High', value: vulnerabilityData?.high || 0, color: '#a83246' },
    { name: 'Medium', value: vulnerabilityData?.medium || 0, color: '#d9b55a' },
    { name: 'Low', value: vulnerabilityData?.low || 0, color: '#5a9178' },
    { name: 'Unknown', value: vulnerabilityData?.unknown || 0, color: '#808080' }
  ];
  
  const totalVulnerabilities = chartData.reduce((sum, item) => sum + item.value, 0);
  
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
        data={chartData} 
        total={totalVulnerabilities}
        width={150}
        height={185}
      />
    </div>
  </div>
      </CardBody>
    </Card>
    </Link>
  );
};

export default AssetCard;