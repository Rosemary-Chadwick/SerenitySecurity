import React, { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { deleteAsset, getAssetById } from "../../managers/assetManager";
import { scanAssetForVulnerabilities } from "../../managers/vulnerabilityManager";
import { deleteReport } from "../../managers/reportManager";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

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
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);

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
    if (window.confirm("Are you sure you want to delete this asset?")) { //  and all its reports
      deleteAsset(asset.id).then(() => {
        window.alert("Asset successfully deleted!");
        navigate('/');
      });
    }
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

  if (isLoading) {
    return <div>Loading asset details...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!asset) {
    return <div>Asset not found</div>;
  }

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>{asset.systemName}</h2>
        <button 
          onClick={() => navigate("/assets")}
          className="btn btn-secondary"
        >
          Back to Assets
        </button>
      </div>

      <div className="card mb-4">
        <div className="card-header">
          <h4>Asset Information</h4>
        </div>
        <div className="card-body">
          <p><strong>IP Address:</strong> {asset.ipAddress}</p>
          <p><strong>OS Version:</strong> {asset.osVersion}</p>
          <p><strong>System Type:</strong> {asset.systemTypeName}</p>
          <p><strong>Status:</strong> {asset.isActive ? "Active" : "Inactive"}</p>
          <p><strong>Created:</strong> {formatDate(asset.createdAt)}</p>
        </div>
      </div>

      <div className="d-flex mb-3">
        <Link to={`/assets/edit/${asset.id}`} className="btn btn-primary me-2">
          Update Asset
        </Link>
        <button 
          className="btn btn-primary" 
          onClick={handleScan}
          disabled={scanning}
        >
          {scanning ? "Scanning..." : "Scan for Vulnerabilities"}
        </button>
                    {/* <button 
              className="btn btn-secondary" 
              onClick={() => {
                fetch('/api/vulnerability/test-connection')
                  .then(res => res.json())
                  .then(data => console.log('API test result:', data))
                  .catch(err => console.error('API test error:', err));
              }}
            >
              Test NVD API
            </button> */}
        <button 
          onClick={handleDelete}
          className="btn btn-danger"
        >
          Delete Asset
        </button>
      </div>
      
      <div className="card">
        <div className="card-header d-flex justify-content-between align-items-center">
          <h4>Report History</h4>
          <select 
            value={filterType} 
            onChange={handleFilterChange}
            className="form-select w-auto"
          >
            <option value="recent">Most Recent</option>
            <option value="inProgress">In Progress</option>
            <option value="completed">Completed</option>
          </select>
        </div>
        <div className="card-body">
          {filteredReports.length > 0 ? (
            <table className="table table-striped">
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredReports.map(report => (
                  <tr key={report.id}>
                    <td>{formatDate(report.createdAt)}</td>
                    <td>{report.isCompleted ? "Completed" : "In Progress"}</td>
                    <td>
                      <Link to={`/report/${report.id}`} className="btn btn-info btn-sm">
                        View Details
                      </Link>
                      <button 
                        className="btn btn-danger btn-sm" 
                        onClick={() => handleReportDeleteClick(report)}
                      >
                        Delete Report
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No reports match your filter. {asset.reports.length === 0 ? "Create a new report to check for vulnerabilities." : ""}</p>
          )}
        </div>
      </div>

      <Modal isOpen={showDeleteModal} toggle={toggleDeleteModal}>
        <ModalHeader toggle={toggleDeleteModal}>Confirm Report Deletion</ModalHeader>
        <ModalBody>
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
        <ModalFooter>
          <Button color="secondary" onClick={toggleDeleteModal} disabled={isDeleting}>
            Cancel
          </Button>
          <Button color="danger" onClick={handleReportDeleteConfirm} disabled={isDeleting}>
            {isDeleting ? "Deleting..." : "Delete Report"}
          </Button>
        </ModalFooter>
      </Modal>

    </div>
  );
};