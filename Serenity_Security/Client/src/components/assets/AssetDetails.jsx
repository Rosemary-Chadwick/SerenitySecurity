import React, { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { deleteAsset, getAssetById } from "../../managers/assetManager";
import { scanAssetForVulnerabilities } from "../../managers/vulnerabilityManager";
import { deleteReport } from "../../managers/reportManager";
import {  Button, Card, CardBody, CardHeader, CardTitle, Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import { useTheme } from '../theme/ThemeContext'; 


export const AssetDetails = () => {
  const [asset, setAsset] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filteredReports, setFilteredReports] = useState([]);
  const [filterType, setFilterType] = useState("recent"); // recent is default : !completed
  const { id } = useParams();
  const navigate = useNavigate();
  const [scanning, setScanning] = useState(false);
  const [reportToDelete, setReportToDelete] = useState(null);
  const [isDeletingAsset, setIsDeletingAsset] = useState(false);
  const [showAssetDeleteModal, setShowAssetDeleteModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const { colors } = useTheme();

  useEffect(() => { // this one sets all the data in state
    setIsLoading(true);
    getAssetById(id)
      .then((data) => {
        setAsset(data);
        setFilteredReports(data.reports); // with all reports
        setIsLoading(false); 
      })
      .catch((err) => {
        setError(err.message);
        setIsLoading(false);
      });
  }, [id]);

  useEffect(() => { // this one sets the filtered results
    if (!asset) return;
    
    let reports = [...asset.reports];
    
    if (filterType === "completed") {
      reports = reports.filter(r => r.isCompleted);
    } else if (filterType === "inProgress") {
      reports = reports.filter(r => !r.isCompleted);
    } else {
      reports.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
    }
    
    setFilteredReports(reports);
  }, [asset, filterType]);

  const handleFilterChange = (e) => {
    setFilterType(e.target.value);
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString();
  };

  const handleDelete = () => {
    setShowAssetDeleteModal(true);
  };

  const handleScan = () => {
    setScanning(true);
    scanAssetForVulnerabilities(asset.id)
      .then((data) => {
        setScanning(false);
        // Navigate to the new report
        navigate(`/report/${data.reportId}`);
      })
      .catch((err) => {
        setScanning(false);
        setError("Failed to scan for vulnerabilities: " + err.message);
      });
  };

  const handleAssetDeleteConfirm = () => {
    setIsDeletingAsset(true);
    
    deleteAsset(asset.id)
      .then(() => {
        setIsDeletingAsset(false);
        setShowAssetDeleteModal(false);
        navigate('/');
      })
      .catch((err) => {
        setError(`Failed to delete asset: ${err.message}`);
        setIsDeletingAsset(false);
      });
  };

  const handleReportDeleteClick = (report) => {
    setReportToDelete(report);
    setShowDeleteModal(true);
  };

  const handleReportDeleteConfirm = () => {
    if (!reportToDelete) return;

    setIsDeleting(true);
    deleteReport(reportToDelete.id)
      .then(() => {
        // Update the asset state to remove the deleted report
        const updatedReports = asset.reports.filter(r => r.id !== reportToDelete.id);
        setAsset({
          ...asset,
          reports: updatedReports
        });
        setFilteredReports(updatedReports);
        setShowDeleteModal(false);
        setReportToDelete(null);
        setIsDeleting(false);
      })
      .catch((err) => {
        setError(`Failed to delete report: ${err.message}`);
        setShowDeleteModal(false);
        setIsDeleting(false);
      });
  };

  const toggleDeleteModal = () => {
    setShowDeleteModal(!showDeleteModal);
    if (!showDeleteModal) {
      setReportToDelete(null);
    }
  };

  const toggleAssetDeleteModal = () => {
    setShowAssetDeleteModal(!showAssetDeleteModal);
  };

  if (isLoading) {
    return <div>Loading asset details...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!asset) {
    return <div>Asset not found</div>;
  }

  const TrashIcon = () => (
    <svg 
      xmlns="http://www.w3.org/2000/svg" 
      width="30"  // Increased from 16
      height="30" // Increased from 16
      fill="#a83246" 
      viewBox="0 0 16 16"
    >
      <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
      <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/>
    </svg>
  );
  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2 style={{ color: colors.cardBg }}>
          {asset.systemName}
        </h2>
        <Button 
          onClick={() => navigate("/assets")}
          style={{
            backgroundColor: colors.buttonHighlight, // Yellow background
            color: colors.primary, // Navy text
            border: 'none',
            fontWeight: 'bold'
          }}
        >
          Back to Assets
        </Button>
      </div>

      {/* Asset Information Card */}
      <Card className="mb-4 shadow">
        <CardHeader style={{ 
          backgroundColor: colors.primary,
          color: colors.secondary,
          borderTopLeftRadius: 'inherit',
          borderTopRightRadius: 'inherit' 
        }}>
          <CardTitle tag="h5" className="mb-0">
            Asset Information
          </CardTitle>
        </CardHeader>
        
        <CardBody style={{ 
          backgroundColor: colors.cardBg,
          color: colors.cardText 
        }}>
          <p><strong>IP Address:</strong> {asset.ipAddress}</p>
          <p><strong>OS Version:</strong> {asset.osVersion}</p>
          <p><strong>System Type:</strong> {asset.systemTypeName}</p>
          <p><strong>Status:</strong> {asset.isActive ? "Active" : "Inactive"}</p>
          <p><strong>Created:</strong> {formatDate(asset.createdAt)}</p>
        </CardBody>
      </Card>

      {/* Action Buttons */}
      <div className="d-flex mb-3 gap-2">
        <Link to={`/assets/edit/${asset.id}`} style={{
          backgroundColor: colors.buttonHighlight,
          color: colors.primary,
          padding: '0.375rem 0.75rem',
          borderRadius: '0.25rem',
          textDecoration: 'none',
          display: 'inline-block'
        }}>
          Update Asset
        </Link>
        
        <Button 
          onClick={handleScan}
          disabled={scanning}
          style={{
            backgroundColor: colors.buttonHighlight,
            color: colors.primary,
            border: 'none'
          }}
        >
          {scanning ? "Scanning..." : "Scan for Vulnerabilities"}
        </Button>
        
        <Button 
  onClick={handleDelete}
  style={{
    backgroundColor: colors.buttonHighlight, // Yellow background
    border: 'none',
    padding: '0.5rem',  // Slightly more padding
    borderRadius: '0.25rem'
  }}
>
          <TrashIcon />
        </Button>
      </div>
      
      {/* Report History Card */}
      <Card className="shadow">
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
      Report History
    </CardTitle>
    <div style={{
      backgroundColor: colors.primary, // Navy background for dropdown
      borderRadius: '0.25rem',
      padding: '0.25rem'
    }}>
      <select 
        value={filterType} 
        onChange={handleFilterChange}
        className="form-select"
        style={{
          width: 'auto',
          backgroundColor: colors.primary, // Navy background
          color: colors.secondary, // Yellow text
          borderColor: colors.secondary,
          fontWeight: 'bold'
        }}
      >
        <option value="recent">Most Recent</option>
        <option value="inProgress">In Progress</option>
        <option value="completed">Completed</option>
      </select>
    </div>
  </CardHeader>
  
  <CardBody style={{ 
    backgroundColor: colors.cardBg, // Using the theme's cardBg
    color: colors.cardText 
  }}>
    {filteredReports.length > 0 ? (
      <table className="table" style={{ 
        color: colors.cardText,
        backgroundColor: colors.cardBg // Using the cream color from the theme
      }}>
        <thead>
          <tr style={{ 
            backgroundColor: '#cad8e7', // The lighter navy color you specified
            color: colors.primary  // Dark navy text
          }}>
            <th style={{ padding: '0.75rem' }}>Date</th>
            <th style={{ padding: '0.75rem' }}>Status</th>
            <th style={{ padding: '0.75rem' }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredReports.map(report => (
            <tr key={report.id} style={{ borderColor: '#cad8e7' }}>
              <td>{formatDate(report.createdAt)}</td>
              <td>{report.isCompleted ? "Completed" : "In Progress"}</td>
              <td>
                <div className="d-flex gap-2">
                  <Link to={`/report/${report.id}`} style={{
                    backgroundColor: colors.primary,
                    color: colors.secondary,
                    padding: '0.25rem 0.5rem',
                    borderRadius: '0.2rem',
                    fontSize: '0.875rem',
                    textDecoration: 'none',
                    display: 'inline-block'
                  }}>
                    View Details
                  </Link>
                  
                  <Button 
                    onClick={() => handleReportDeleteClick(report)}
                    style={{
                      backgroundColor: colors.buttonHighlight, // Yellow background
                      border: 'none',
                      padding: '0.25rem 0.5rem',
                      borderRadius: '0.25rem'
                    }}
                  >
                    <TrashIcon />
                  </Button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    ) : (
      <p>No reports match your filter. {asset.reports.length === 0 ? "Create a new report to check for vulnerabilities." : ""}</p>
    )}
  </CardBody>
</Card>

      {/* Keep your existing Modal component with some theme styling */}
      <Modal isOpen={showDeleteModal} toggle={toggleDeleteModal}>
        <ModalHeader toggle={toggleDeleteModal} style={{
          backgroundColor: colors.primary,
          color: colors.secondary
        }}>
          Confirm Report Deletion
        </ModalHeader>
        <ModalBody style={{
          backgroundColor: colors.cardBg,
          color: colors.cardText
        }}>
          <div className="alert alert-danger">
            <p><strong>Warning:</strong> This action cannot be undone.</p>
            <p>Deleting this report will permanently remove:</p>
            <ul>
              <li>The vulnerability scan report from {reportToDelete ? formatDate(reportToDelete.createdAt) : ''}</li>
              <li>All detected vulnerabilities associated with this report</li>
              <li>Any remediation progress for these vulnerabilities</li>
            </ul>
            <p>Are you sure you want to delete this report?</p>
          </div>
        </ModalBody>
        <ModalFooter style={{
          backgroundColor: colors.cardBg,
          color: colors.cardText
        }}>
          <Button color="secondary" onClick={toggleDeleteModal} disabled={isDeleting} style={{
            backgroundColor: 'transparent',
            color: colors.cardText,
            border: `1px solid ${colors.cardText}`
          }}>
            Cancel
          </Button>
          <Button color="danger" onClick={handleReportDeleteConfirm} disabled={isDeleting}>
            {isDeleting ? "Deleting..." : "Delete Report"}
          </Button>
        </ModalFooter>
      </Modal>
      <Modal isOpen={showAssetDeleteModal} toggle={toggleAssetDeleteModal}>
  <ModalHeader style={{ 
    backgroundColor: colors.primary,
    color: colors.secondary
  }}>
    Confirm Asset Deletion
  </ModalHeader>
  <ModalBody style={{
    backgroundColor: colors.cardBg,
    color: colors.cardText
  }}>
    <div className="alert alert-danger">
      <p><strong>Warning:</strong> This action cannot be undone.</p>
      <p>Deleting this asset will permanently remove:</p>
      <ul>
        <li>The asset "{asset.systemName}" and all its configuration data</li>
        <li>All vulnerability scan reports for this asset</li>
        <li>All remediation progress and history</li>
      </ul>
      <p>Are you sure you want to delete this asset?</p>
    </div>
  </ModalBody>
  <ModalFooter style={{
    backgroundColor: colors.cardBg,
    color: colors.cardText
  }}>
    <Button 
      onClick={toggleAssetDeleteModal} 
      disabled={isDeletingAsset}
      style={{
        backgroundColor: 'transparent',
        color: colors.cardText,
        border: `1px solid ${colors.cardText}`
      }}
    >
      Cancel
    </Button>
    <Button 
      onClick={handleAssetDeleteConfirm} 
      disabled={isDeletingAsset}
      style={{
        backgroundColor: '#dc3545',
        color: 'white',
        border: 'none'
      }}
    >
      {isDeletingAsset ? "Deleting..." : "Delete Asset"}
    </Button>
  </ModalFooter>
</Modal>
    </div>
  );
};