import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import { getReportById } from "../../managers/reportManager";
import { generateRemediationSteps } from "../../managers/vulnerabilityManager";
import { Card, CardBody, CardHeader, CardTitle, Button } from "reactstrap";
import { useTheme } from '../theme/ThemeContext';

export const ReportDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [report, setReport] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const { colors } = useTheme();

  useEffect(() => {
    getReportById(id)
      .then((data) => {
        setReport(data);
      // Check if report has vulnerabilities that might need remediation enhancement
      if (data.vulnerabilities && data.vulnerabilities.length > 0) {
        // Process each vulnerability to ensure it has detailed remediation steps
        const promises = data.vulnerabilities.map(vulnerability => {
          // Check if this vulnerability might need enhanced remediation
          if (!vulnerability.hasDetailedRemediation) {
            // This is a flag you can add to your backend DTO to indicate if detailed remediation exists
            return generateRemediationSteps(vulnerability.id, true)
              .then(() => vulnerability)
              .catch(err => {
                console.error(`Error generating remediation for ${vulnerability.cveId}:`, err);
                return vulnerability;
              });
          }
          return Promise.resolve(vulnerability);
        });
        
        // Wait for all remediation generations to complete
        Promise.all(promises)
          .then(() => {
            console.log("All vulnerabilities have detailed remediation steps");
            setLoading(false);
          });
      } else {
        setLoading(false);
      }
    })
      .catch((err) => {
        setError("Failed to load report data: " + err.message);
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <div>Loading report data...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (!report) {
    return <div>Report not found</div>;
  }

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
  };

  const getSeverityColor = (severity) => {
    // Convert to lowercase for case-insensitive comparison
    const level = severity?.toLowerCase() || '';
    
    if (level.includes('high') || level.includes('critical'))
      return { bg: '#a83246', text: 'white' }; // Muted red
    
    if (level.includes('medium'))
      return { bg: '#d9b55a', text: '#1b2a3c' }; // Muted yellow with dark text
    
    if (level.includes('low'))
      return { bg: '#5a9178', text: 'white' }; // Muted green
    
    // Default fallback
    return { bg: '#6c757d', text: 'white' }; // Gray for unknown
  };

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2 style={{ color: colors.cardBg }}>
          Vulnerability Report
        </h2>
        <Button 
          onClick={() => navigate(`/assets/${report.asset.id}`)}
          style={{
            backgroundColor: colors.buttonHighlight, // Yellow background
            color: colors.primary, // Navy text
            border: 'none',
            fontWeight: 'bold'
          }}
        >
          Back to Asset
        </Button>
      </div>

      {/* Report Details Card */}
      <Card className="mb-4 shadow">
        <CardHeader style={{ 
          backgroundColor: colors.primary,
          color: colors.secondary,
          borderTopLeftRadius: 'inherit',
          borderTopRightRadius: 'inherit' 
        }}>
          <CardTitle tag="h5" className="mb-0">
            Report Details
          </CardTitle>
        </CardHeader>
        
        <CardBody style={{ 
          backgroundColor: colors.cardBg,
          color: colors.cardText 
        }}>
          <p><strong>Asset Name:</strong> {report.asset.systemName}</p>
          <p><strong>Report Date:</strong> {formatDate(report.createdAt)}</p>
          <p><strong>IP Address:</strong> {report.asset.ipAddress}</p>
          <p><strong>OS Version:</strong> {report.asset.osVersion}</p>
        </CardBody>
      </Card>

      {/* Vulnerabilities Found Card */}
      <Card className="shadow">
        <CardHeader style={{ 
          backgroundColor: colors.primary,
          color: colors.secondary,
          borderTopLeftRadius: 'inherit',
          borderTopRightRadius: 'inherit' 
        }}>
          <CardTitle tag="h5" className="mb-0">
            Vulnerabilities Found
          </CardTitle>
        </CardHeader>
        
        <CardBody style={{ 
          backgroundColor: colors.cardBg,
          color: colors.cardText 
        }}>
          {report.vulnerabilities && report.vulnerabilities.length > 0 ? (
            <table className="table" style={{ 
              color: colors.cardText,
              backgroundColor: colors.cardBg // Using the cream color from the theme
            }}>
              <thead>
                <tr style={{ 
                  backgroundColor: '#cad8e7', // The lighter navy color you specified
                  color: colors.primary  // Dark navy text
                }}>
                  <th style={{ padding: '0.75rem' }}>CVE ID</th>
                  <th style={{ padding: '0.75rem' }}>Severity Level</th>
                  <th style={{ padding: '0.75rem' }}>CVSS Score</th>
                  <th style={{ padding: '0.75rem' }}>Actions</th>
                </tr>
              </thead>
              <tbody>
                {report.vulnerabilities.map((vulnerability) => (
                  <tr key={vulnerability.id} style={{ borderColor: '#cad8e7' }}>
                    <td>{vulnerability.cveId}</td>
                    <td>
  {vulnerability.severityLevel && (
    <div 
      className="severity-badge"
      style={{
        display: 'inline-block',
        padding: '0.25rem 0.5rem',
        borderRadius: '0.25rem',
        fontSize: '0.75rem',
        fontWeight: 'bold',
        backgroundColor: getSeverityColor(vulnerability.severityLevel).bg,
        color: getSeverityColor(vulnerability.severityLevel).text
      }}
    >
      {vulnerability.severityLevel}
    </div>
  )}
</td>
                    <td>{vulnerability.cvsScore.toFixed(1)}</td>
                    <td>
                      <Link
                        to={`/vulnerability/${vulnerability.id}?reportId=${report.id}`}
                        style={{
                          backgroundColor: colors.primary,
                          color: colors.secondary,
                          padding: '0.25rem 0.5rem',
                          borderRadius: '0.2rem',
                          fontSize: '0.875rem',
                          textDecoration: 'none',
                          display: 'inline-block'
                        }}
                      >
                        View Details
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No vulnerabilities found for this asset.</p>
          )}
        </CardBody>
      </Card>
    </div>
  );
};