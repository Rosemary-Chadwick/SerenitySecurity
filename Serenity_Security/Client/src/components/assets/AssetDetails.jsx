import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { getAssetById } from "../../managers/assetManager";

export const AssetDetails = () => {
    const [asset, setAsset] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [filteredReports, setFilteredReports] = useState([]);
    const [filterType, setFilterType] = useState("recent"); // recent is default : !completed
    const { id } = useParams();

  useEffect(() => { // this one sets all the data in state
    setIsLoading(true);
    getAssetById(id)
      .then((data) => {
        setAsset(data);
        setFilteredReports(data.reports); // all reports
        setIsLoading(false); // turns off the loading indicator 
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
    <div className="asset-details">
      <h2>{asset.systemName}</h2>
      <div className="asset-info">
        <p><strong>IP Address:</strong> {asset.ipAddress}</p>
        <p><strong>OS Version:</strong> {asset.osVersion}</p>
        <p><strong>System Type:</strong> {asset.systemTypeName}</p>
        <p><strong>Status:</strong> {asset.isActive ? "Active" : "Inactive"}</p>
        <p><strong>Created:</strong> {new Date(asset.createdAt).toLocaleDateString()}</p>
      </div>
      
      <div className="asset-actions">
        <Link to={`/assets/edit/${asset.id}`} className="btn">Edit Asset</Link>
        <Link to={`/assets/report/create/${asset.id}`} className="btn">Create New Report</Link>
      </div>
      
      <div className="reports-section">
        <h3>Reports</h3>
        
        <div className="report-filters">
          <select 
            value={filterType} 
            onChange={handleFilterChange}
            className="filter-select"
          >
            <option value="recent">Most Recent</option>
            <option value="inProgress">In Progress</option>
            <option value="completed">Completed</option>
          </select>
        </div>
        
        {filteredReports.length > 0 ? (
          <table className="report-table">
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
                  <td>{new Date(report.createdAt).toLocaleDateString()}</td>
                  <td>{report.isCompleted ? "Completed" : "In Progress"}</td>
                  <td>
                    <Link to={`/reports/${report.id}`}>View Details</Link>
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
  );
};