import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link, useLocation } from "react-router-dom";
import { getRemediationById, toggleRemediationItem } from "../../managers/remediationManager";
import { Card, CardBody, CardHeader, CardTitle, Button } from "reactstrap";
import { useTheme } from '../theme/ThemeContext';

export const RemediationDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const [remediation, setRemediation] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [updating, setUpdating] = useState(false);
  const [remediationType, setRemediationType] = useState("generic");
  const [osType, setOsType] = useState("generic");
  const { colors } = useTheme();

  const getReportId = () => {
    const searchParams = new URLSearchParams(location.search);
    return searchParams.get('reportId');
  };

  const getVulnerabilityId = () => {
    const searchParams = new URLSearchParams(location.search);
    return searchParams.get('vulnerabilityId');
  };

  useEffect(() => {
    getRemediationById(id)
      .then((data) => {
        console.log("Remediation data:", data); // Debug log
        setRemediation(data);
        
        // Determine remediation type based on description
        const type = determineRemediationType(data.description);
        console.log("Determined type:", type); // Debug log
        setRemediationType(type);
        
        // Determine OS type if possible
        const os = determineOsType(data.description, data.verificationSteps);
        console.log("Determined OS:", os); // Debug log
        setOsType(os);
        
        setLoading(false);
      })
      .catch((err) => {
        console.error("Error loading remediation:", err); // Debug log
        setError("Failed to load remediation data: " + err.message);
        setLoading(false);
      });
  }, [id]);

  // Helper function to determine remediation type from description
  const determineRemediationType = (description) => {
    if (!description) return "generic";
    
    const desc = description.toLowerCase();
    
    if (desc.includes("command injection") || 
        desc.includes("shell") || 
        desc.includes("csrf") ||
        desc.includes("input validation")) {
      return "commandInjection";
    }
    
    if (desc.includes("memory corruption") || 
        desc.includes("buffer overflow") || 
        desc.includes("denial of service")) {
      return "memoryCorruption";
    }
    
    if (desc.includes("privilege") || 
        desc.includes("permission") || 
        desc.includes("access control")) {
      return "accessControl";
    }
    
    if (desc.includes("network") || 
        desc.includes("router") || 
        desc.includes("firewall")) {
      return "networkSecurity";
    }
    
    return "generic";
  };

  // Helper function to determine OS type
  const determineOsType = (description, verificationSteps) => {
    if (!description && !verificationSteps) return "generic";
    
    const desc = (description || "").toLowerCase();
    const steps = (verificationSteps || "").toLowerCase();
    
    if (desc.includes("ubuntu") || 
        desc.includes("linux") || 
        steps.includes("apt") || 
        steps.includes("sudo")) {
      return "linux";
    }
    
    if (desc.includes("windows") || 
        steps.includes("hotfix") || 
        steps.includes("get-hotfix")) {
      return "windows";
    }
    
    if (desc.includes("network device") || 
        desc.includes("router") || 
        desc.includes("firewall")) {
      return "network";
    }
    
    return "generic";
  };

  const handleToggleCompletion = (isCompleted) => {
    setUpdating(true);
    toggleRemediationItem(remediation.id, isCompleted)
      .then((updatedItem) => {
        setRemediation({ ...remediation, isCompleted });
        setUpdating(false);
      })
      .catch((err) => {
        setError("Failed to update remediation status: " + err.message);
        setUpdating(false);
      });
  };

  // Render customized remediation guide based on type
  const renderRemediationGuide = () => {
    if (!remediation) return null;
    
    // Guide layout container with styling for all guide types
    const GuideContainer = ({ title, children }) => (
      <div className="alert" style={{
        backgroundColor: colors.isDarkMode ? '#264f78' : '#e3f2fd',
        color: colors.primary,
        borderColor: colors.isDarkMode ? '#90caf9' : '#1976d2',
        borderRadius: '0.25rem',
        padding: '1.25rem'
      }}>
        <h5 className="alert-heading" style={{ 
          fontWeight: 'bold', 
          marginBottom: '1rem',
          color: colors.isDarkMode ? colors.secondary : colors.primary 
        }}>
          {title}
        </h5>
        {children}
      </div>
    );
    
    // Code block styling for verification steps
    const CodeBlock = ({ children }) => (
      <div className="card mt-3 mb-3">
        <div className="card-body" style={{
          backgroundColor: colors.isDarkMode ? '#2d3748' : '#f8f9fa',
          color: colors.isDarkMode ? '#f7fafc' : '#1a202c',
          padding: '1rem',
          borderRadius: '0.25rem',
          fontFamily: 'monospace',
          overflowX: 'auto'
        }}>
          <pre className="mb-0" style={{ whiteSpace: 'pre-wrap' }}>{children}</pre>
        </div>
      </div>
    );
    
    switch (remediationType) {
      case "commandInjection":
        return (
          <GuideContainer title="Command Injection Remediation">
            <p>Address command injection vulnerabilities with these steps:</p>
            
            <ol className="mt-3">
              <li><strong>Input Validation</strong>
                <p>Implement strict input validation to filter out malicious characters.</p>
                <p>Validate input against a whitelist of allowed characters.</p>
              </li>
              
              <li><strong>Use Parameterized APIs</strong>
                <p>Avoid directly passing user input to command execution functions.</p>
                <p>Use library functions that handle command parameters safely.</p>
              </li>
              
              <li><strong>Implement Context-Specific Output Encoding</strong>
                <p>Encode user-supplied data before including it in command parameters.</p>
              </li>
              
              <li><strong>Apply Security Patches</strong>
                {remediation.fixedVersion && 
                  <p>Update to version: <strong>{remediation.fixedVersion}</strong></p>
                }
                <p>Keep all system components and libraries updated.</p>
              </li>
            </ol>
            
            {renderOsSpecificSteps()}
          </GuideContainer>
        );
      
      case "memoryCorruption":
        return (
          <GuideContainer title="Memory Corruption Remediation">
            <p>Fix memory corruption issues with these steps:</p>
            
            <ol className="mt-3">
              <li><strong>Apply Security Patches</strong>
                <p>Update the affected software to the latest security patch.</p>
                {remediation.fixedVersion && 
                  <p>The vulnerability is fixed in version: <strong>{remediation.fixedVersion}</strong></p>
                }
              </li>
              
              <li><strong>Enable Memory Protection</strong>
                <p>Enable Address Space Layout Randomization (ASLR), Data Execution Prevention (DEP), and other memory protection mechanisms.</p>
              </li>
              
              <li><strong>Manage Resource Allocation</strong>
                <p>Implement resource quotas and limits to prevent resource exhaustion attacks.</p>
              </li>
              
              <li><strong>Deploy Runtime Application Self-Protection</strong>
                <p>Consider using RASP solutions to detect and prevent exploitation attempts.</p>
              </li>
            </ol>
            
            {renderOsSpecificSteps()}
          </GuideContainer>
        );
      
      case "accessControl":
        return (
          <GuideContainer title="Access Control Remediation">
            <p>Address privilege and access control issues with these steps:</p>
            
            <ol className="mt-3">
              <li><strong>Apply Principle of Least Privilege</strong>
                <p>Review and restrict permissions to the minimum required for functionality.</p>
              </li>
              
              <li><strong>Implement Strong Access Controls</strong>
                <p>Use role-based access control and proper authorization checks.</p>
              </li>
              
              <li><strong>Apply Security Patches</strong>
                {remediation.fixedVersion && 
                  <p>Update to version: <strong>{remediation.fixedVersion}</strong></p>
                }
              </li>
              
              <li><strong>Audit Authentication Mechanisms</strong>
                <p>Ensure strong authentication is enforced for privileged operations.</p>
              </li>
            </ol>
            
            {renderOsSpecificSteps()}
          </GuideContainer>
        );
      
      case "networkSecurity":
        return (
          <GuideContainer title="Network Security Remediation">
            <p>Address network-related vulnerabilities with these steps:</p>
            
            <ol className="mt-3">
              <li><strong>Update Firmware</strong>
                <p>Apply the latest firmware updates for all network devices.</p>
                {remediation.fixedVersion && 
                  <p>The vulnerability is fixed in version: <strong>{remediation.fixedVersion}</strong></p>
                }
              </li>
              
              <li><strong>Implement Network Segmentation</strong>
                <p>Segment networks to limit lateral movement in case of compromise.</p>
              </li>
              
              <li><strong>Configure Proper Access Controls</strong>
                <p>Restrict management interfaces to authorized IPs and implement strong authentication.</p>
              </li>
              
              <li><strong>Enable Encryption</strong>
                <p>Use encrypted protocols for all sensitive communications.</p>
              </li>
            </ol>
            
            {renderOsSpecificSteps()}
          </GuideContainer>
        );
      
      default:
        // Fall back to the generic guide but with dynamic content
        return (
          <GuideContainer title="Remediation Instructions">
            <p>Below are remediation steps for this vulnerability:</p>
            
            <ol className="mt-3">
              <li><strong>Update Affected Systems</strong>
                <p>Apply the latest security patches from your vendor.</p>
                {remediation.fixedVersion && 
                  <p>The vulnerability is fixed in version: <strong>{remediation.fixedVersion}</strong></p>
                }
              </li>
              
              <li><strong>Verify Resolution</strong>
                <p>After applying updates, verify the vulnerability has been remediated using the steps below.</p>
                {remediation.verificationSteps && <CodeBlock>{remediation.verificationSteps}</CodeBlock>}
              </li>
              
              <li><strong>Document Actions</strong>
                <p>Document all remediation actions taken for audit and compliance purposes.</p>
              </li>
            </ol>
            
            {renderOsSpecificSteps()}
          </GuideContainer>
        );
    }
  };

  // Render OS-specific guidance with styled code blocks
  const renderOsSpecificSteps = () => {
    if (!remediation) return null;
    
    // Code block styling consistent with other blocks
    const CodeBlock = ({ children }) => (
      <div className="card">
        <div className="card-body" style={{
          backgroundColor: colors.isDarkMode ? '#2d3748' : '#f8f9fa',
          color: colors.isDarkMode ? '#f7fafc' : '#1a202c',
          padding: '1rem',
          borderRadius: '0.25rem',
          fontFamily: 'monospace',
          overflowX: 'auto'
        }}>
          <pre className="mb-0" style={{ whiteSpace: 'pre-wrap' }}>{children}</pre>
        </div>
      </div>
    );
    
    switch (osType) {
      case "linux":
        return (
          <div className="mt-4">
            <h6 style={{ color: colors.isDarkMode ? colors.secondary : colors.primary }}>
              Linux-Specific Instructions:
            </h6>
            <CodeBlock>
{`# Update package information
sudo apt update

# Apply security updates
sudo apt upgrade -y

# Check for vulnerable packages
dpkg -l | grep [package-name]

# Verify system security
${remediation.verificationSteps || "# Run system-specific verification commands"}`}
            </CodeBlock>
          </div>
        );
      
      case "windows":
        return (
          <div className="mt-4">
            <h6 style={{ color: colors.isDarkMode ? colors.secondary : colors.primary }}>
              Windows-Specific Instructions:
            </h6>
            <CodeBlock>
{`# Check for installed updates
Get-Hotfix

# Verify specific security patch
Get-Hotfix -Id "KB*****"

# Check system security configuration
${remediation.verificationSteps || "# Run system-specific verification commands"}`}
            </CodeBlock>
          </div>
        );
      
      case "network":
        return (
          <div className="mt-4">
            <h6 style={{ color: colors.isDarkMode ? colors.secondary : colors.primary }}>
              Network Device Instructions:
            </h6>
            <CodeBlock>
{`# Check current firmware version
show version

# Verify configuration
show running-config

# Additional verification steps
${remediation.verificationSteps || "# Run device-specific verification commands"}`}
            </CodeBlock>
          </div>
        );
      
      default:
        return remediation.verificationSteps ? (
          <div className="mt-4">
            <h6 style={{ color: colors.isDarkMode ? colors.secondary : colors.primary }}>
              Verification Steps:
            </h6>
            <CodeBlock>{remediation.verificationSteps}</CodeBlock>
          </div>
        ) : null;
    }
  };

  if (loading) {
    return <div className="container mt-4">Loading remediation data...</div>;
  }

  if (error) {
    return <div className="container mt-4 alert alert-danger">{error}</div>;
  }

  if (!remediation) {
    return <div className="container mt-4">Remediation not found</div>;
  }

  return (
    <div className="container mt-4">
      {/* Page Header */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2 style={{ color: colors.colorbg }}>
          Remediation Details
        </h2>
        <div className="d-flex gap-2">
          {getVulnerabilityId() && (
            <Link 
              to={`/vulnerability/${getVulnerabilityId()}?reportId=${getReportId() || ''}`} 
              style={{
                backgroundColor: colors.buttonHighlight,
                color: colors.primary,
                padding: '0.375rem 0.75rem',
                borderRadius: '0.25rem',
                textDecoration: 'none',
                display: 'inline-block',
                fontWeight: 'bold',
                border: 'none'
              }}
            >
              Back to Vulnerability
            </Link>
          )}
          {!getVulnerabilityId() && (
            <Button 
              onClick={() => navigate(-1)}
              style={{
                backgroundColor: colors.buttonHighlight,
                color: colors.primary,
                border: 'none',
                fontWeight: 'bold'
              }}
            >
              Back to Vulnerability
            </Button>
          )}
        </div>
      </div>

      {/* Remediation Steps Card */}
      <Card className="mb-4 shadow">
        <CardHeader style={{ 
          backgroundColor: colors.primary,
          color: colors.secondary,
          borderTopLeftRadius: 'inherit',
          borderTopRightRadius: 'inherit',
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center'
        }}>
          <CardTitle tag="h5" className="mb-0">
            Remediation Steps
          </CardTitle>
          <span style={{
            display: 'inline-block',
            padding: '0.25rem 0.75rem',
            borderRadius: '0.25rem',
            fontSize: '0.875rem',
            fontWeight: 'bold',
            backgroundColor: remediation.isCompleted ? '#5a9178' : '#d9b55a',
            color: remediation.isCompleted ? 'white' : '#1b2a3c'
          }}>
            {remediation.isCompleted ? 'Completed' : 'Pending'}
          </span>
        </CardHeader>
        
        <CardBody style={{ 
          backgroundColor: colors.cardBg,
          color: colors.cardText 
        }}>
          <div className="mb-4">
            <div className="form-check form-switch">
              <input
                className="form-check-input"
                type="checkbox"
                id="status-toggle"
                checked={remediation.isCompleted}
                onChange={(e) => handleToggleCompletion(e.target.checked)}
                disabled={updating}
                style={{ cursor: 'pointer' }}
              />
              <label className="form-check-label" htmlFor="status-toggle" style={{ cursor: 'pointer' }}>
                Mark as {remediation.isCompleted ? 'incomplete' : 'complete'}
              </label>
            </div>
          </div>

          <div className="row mb-3">
            <div className="col-md-3">
              <strong>Description:</strong>
            </div>
            <div className="col-md-9">
              {remediation.description}
            </div>
          </div>

          {remediation.fixedVersion && (
            <div className="row mb-3">
              <div className="col-md-3">
                <strong>Fixed Version:</strong>
              </div>
              <div className="col-md-9">
                {remediation.fixedVersion}
              </div>
            </div>
          )}

          {remediation.verificationSteps && (
            <div className="row mb-3">
              <div className="col-md-3">
                <strong>Verification Steps:</strong>
              </div>
              <div className="col-md-9">
                <div className="card">
                  <div className="card-body" style={{
                    backgroundColor: colors.isDarkMode ? '#2d3748' : '#f8f9fa',
                    color: colors.isDarkMode ? '#f7fafc' : '#1a202c',
                    fontFamily: 'monospace',
                    overflowX: 'auto'
                  }}>
                    <pre className="mb-0" style={{ whiteSpace: 'pre-wrap' }}>{remediation.verificationSteps}</pre>
                  </div>
                </div>
              </div>
            </div>
          )}

          <div className="row mb-3">
            <div className="col-md-3">
              <strong>Created:</strong>
            </div>
            <div className="col-md-9">
              {new Date(remediation.createdAt).toLocaleString()}
            </div>
          </div>

          <div className="row mb-3">
            <div className="col-md-3">
              <strong>Related CVE:</strong>
            </div>
            <div className="col-md-9">
              <Link 
                to={`/vulnerability/${remediation.vulnerabilityId}`}
                style={{ color: colors.isDarkMode ? '#90caf9' : '#1976d2' }}
              >
                {remediation.vulnerabilityCveId}
              </Link>
            </div>
          </div>
        </CardBody>
      </Card>

      {/* Suggested Remediation Guide Card */}
      <Card className="shadow">
        <CardHeader style={{ 
          backgroundColor: colors.primary,
          color: colors.secondary,
          borderTopLeftRadius: 'inherit',
          borderTopRightRadius: 'inherit' 
        }}>
          <CardTitle tag="h5" className="mb-0">
            Suggested Remediation Guide
          </CardTitle>
        </CardHeader>
        
        <CardBody style={{ 
          backgroundColor: colors.cardBg,
          color: colors.cardText 
        }}>
          {renderRemediationGuide()}
        </CardBody>
      </Card>
    </div>
  );
};