import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import { getReportById } from "../../managers/reportManager";

export const ReportDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [report, setReport] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getReportById(id)
      .then((data) => {
        setReport(data);
        setLoading(false);
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

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Vulnerability Report</h2>
        <button 
          className="btn btn-secondary" 
          onClick={() => navigate(`/assets/${report.asset.id}`)}
        >
          Back to Asset
        </button>
      </div>

      <div className="card mb-4">
        <div className="card-header">
          <h4>Report Details</h4>
        </div>
        <div className="card-body">
          <p><strong>Asset Name:</strong> {report.asset.systemName}</p>
          <p><strong>Report Date:</strong> {formatDate(report.createdAt)}</p>
          <p><strong>IP Address:</strong> {report.asset.ipAddress}</p>
          <p><strong>OS Version:</strong> {report.asset.osVersion}</p>
        </div>
      </div>

      <div className="card">
        <div className="card-header">
          <h4>Vulnerabilities Found</h4>
        </div>
        <div className="card-body">
          {report.vulnerabilities && report.vulnerabilities.length > 0 ? (
            <table className="table table-striped">
              <thead>
                <tr>
                  <th>CVE ID</th>
                  <th>Severity Level</th>
                  <th>CVSS Score</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {report.vulnerabilities.map((vulnerability) => (
                  <tr key={vulnerability.id}>
                    <td>{vulnerability.cveId}</td>
                    <td>{vulnerability.severityLevel}</td>
                    <td>{vulnerability.cvsScore.toFixed(1)}</td>
                    <td>
                      <Link
                        to={`/vulnerability/${vulnerability.id}`}
                        className="btn btn-info btn-sm"
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
        </div>
      </div>
    </div>
  );
};