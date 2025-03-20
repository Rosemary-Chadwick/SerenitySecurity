import { useState, useEffect } from "react";
import { getUserAssets } from "../../managers/assetManager";
import { Link, useNavigate } from "react-router-dom";
import { Container, Row, Col, Button, Card, CardBody, CardTitle } from "reactstrap";
import { useTheme } from "../theme/ThemeContext";
import AssetCard from "./AssetCard";
import DashboardCharts from "../dashboard/DashboardCharts";
import { getAssetVulnerabilityStats } from "../../managers/dashboardManager";

export const AssetList = () => {
  const [assets, setAssets] = useState([]);
  const [vulnerabilityData, setVulnerabilityData] = useState({});
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const { colors } = useTheme();

  const fetchData = () => {
    setIsLoading(true);
    
    Promise.all([
      getUserAssets(),
      getAssetVulnerabilityStats()
    ])
      .then(([assetsData, vulnData]) => {
        const vulnDataMap = {};
        vulnData.forEach(item => {
          vulnDataMap[item.name] = item;
        });
        
        setAssets(assetsData);
        setVulnerabilityData(vulnDataMap);
        setIsLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setIsLoading(false);
      });
  };

  useEffect(() => {
    fetchData();
  }, []);

  if (isLoading) {
    return (
      <Container className="py-4">
        <div className="text-center p-5">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading assets...</span>
          </div>
          <p className="mt-3">Loading assets...</p>
        </div>
      </Container>
    );
  }

  if (error) {
    return (
      <Container className="py-4">
        <Card className="text-center p-4 bg-light">
          <CardBody>
            <CardTitle tag="h4">Error Loading Assets</CardTitle>
            <p>{error}</p>
            <Button color="primary" onClick={fetchAssets}>
              Try Again
            </Button>
          </CardBody>
        </Card>
      </Container>
    );
  }

  return (
    <Container className="py-4 fade-in">
      <Row>
        <Col md={8}>
          <div className="dashboard-header">
            <div className="d-flex justify-content-between align-items-center mb-4">
              <h1 className="mb-0">Security Dashboard</h1>
              <Button
                style={{
                  backgroundColor: colors.buttonHighlight,
                  borderColor: colors.buttonHighlight,
                  color: colors.primary,
                  fontWeight: 'bold'
                }}
                tag={Link}
                to="/assets/create"
                className="d-flex align-items-center"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="16"
                  height="16"
                  fill="currentColor"
                  className="bi bi-plus-circle me-2"
                  viewBox="0 0 16 16"
                >
                  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z" />
                  <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z" />
                </svg>
                Add New Asset
              </Button>
            </div>
          </div>

          <h2 className="mb-3">My Assets</h2>
          {assets.length === 0 ? (
            <Card className="text-center p-5" style={{ backgroundColor: colors.cardBg, color: colors.cardText }}>
              <CardBody>
                <CardTitle tag="h4">No Assets Found</CardTitle>
                <p>You haven't added any assets yet. Add a new asset to get started.</p>
                <Button 
                  style={{
                    backgroundColor: colors.buttonHighlight,
                    borderColor: colors.buttonHighlight,
                    color: colors.primary
                  }}
                  tag={Link} 
                  to="/assets/create"
                >
                  Add Your First Asset
                </Button>
              </CardBody>
            </Card>
          ) : (
<Row>
  {assets.map((asset) => (
    <Col key={asset.id} lg={6} className="mb-4">
      <AssetCard 
        asset={asset} 
        vulnerabilityData={vulnerabilityData[asset.systemName]} 
      />
    </Col>
  ))}
</Row>
          )}
        </Col>
        
        <Col md={4}>
          {/* Side panel for charts */}
          <DashboardCharts />
        </Col>
      </Row>
    </Container>
  );
};