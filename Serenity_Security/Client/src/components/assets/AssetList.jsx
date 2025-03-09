import { useState, useEffect } from "react";
import { getUserAssets } from "../../managers/assetManager";
import { Link, useNavigate } from "react-router-dom";

export const AssetList = () => {
  const [assets, setAssets] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const fetchAssets = () => {
    setIsLoading(true);
    getUserAssets()
      .then((data) => {
        setAssets(data);
        setIsLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setIsLoading(false);
      });
  };

  useEffect(() => {
    fetchAssets();
  }, []);

  if (isLoading) {
    return <div>Loading assets...</div>;
  }

  if (error) {
    return <div>Error loading assets: {error}</div>;
  }

  return (
    <div className="asset-list-container">
      <h2>My Assets</h2>
      <div className="actions">
        <Link to="/assets/create" className="btn">
          Add New Asset
        </Link>
      </div>
      {assets.length === 0 ? (
        <p>No assets found. Add a new asset to get started.</p>
      ) : (
        <div className="asset-grid">
          {assets.map((asset) => (
            <div key={asset.id} className="asset-card">
              <h3>{asset.systemName}</h3>
              <p>IP: {asset.ipAddress}</p>
              <p>Type: {asset.systemTypeName}</p>
              <p>OS: {asset.osVersion}</p>
              <div className="asset-actions">
                <Link to={`/assets/${asset.id}`}>View Details</Link>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};