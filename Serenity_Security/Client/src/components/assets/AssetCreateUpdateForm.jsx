import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createAsset, getAssetById, updateAsset } from '../../managers/assetManager';
import { getAllSystemTypes } from '../../managers/systemTypeManager';

export const AssetForm = () => {
  // Get the id parameter from the URL if it exists
  const { id } = useParams();
  const navigate = useNavigate();
  
  // Determine if this is an edit (id exists) or create (no id) operation
  const isEditMode = !!id;
  
  // State for the form data
  const [asset, setAsset] = useState({
    systemName: '',
    ipAddress: '',
    osVersion: '',
    systemTypeId: 0,
    isActive: true
  });
  
  // State for system types (for the dropdown)
  const [systemTypes, setSystemTypes] = useState([]);
  
  // State for loading status and errors
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  
  // Load system types and, if in edit mode, the existing asset
  useEffect(() => {
    // First load system types for the dropdown
    getAllSystemTypes()
      .then(types => {
        setSystemTypes(types);
        
        // If we're in edit mode, also load the existing asset
        if (isEditMode) {
          return getAssetById(id)
            .then(assetData => {
              // Map the returned asset data to our form state
              setAsset({
                systemName: assetData.systemName,
                ipAddress: assetData.ipAddress,
                osVersion: assetData.osVersion,
                systemTypeId: assetData.systemTypeId, // You may need to adapt this based on your actual DTO
                isActive: assetData.isActive
              });
            })
            .catch(err => {
              setError(`Failed to load asset: ${err.message}`);
            });
        }
      })
      .catch(err => {
        setError(`Failed to load system types: ${err.message}`);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [id, isEditMode]);
  
  // Handle form input changes
  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setAsset({
      ...asset,
      [name]: type === 'checkbox' ? checked : value
    });
  };
  
  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    
    // Convert systemTypeId to a number
    const assetToSave = {
      ...asset,
      systemTypeId: parseInt(asset.systemTypeId, 10)
    };
    
    // Choose whether to create or update based on edit mode
    const saveOperation = isEditMode
      ? updateAsset(id, assetToSave)
      : createAsset(assetToSave);
    
    saveOperation
      .then(() => {
        // Navigate back to the asset list or to the asset details
        navigate(isEditMode ? `/assets/${id}` : '/assets');
      })
      .catch(err => {
        setError(`Failed to save asset: ${err.message}`);
        setLoading(false);
      });
  };
  
  if (loading && isEditMode) {
    return <div>Loading...</div>;
  }
  
  return (
    <div className="container mt-4">
      <h2>{isEditMode ? 'Edit Asset' : 'Create New Asset'}</h2>
      
      {error && <div className="alert alert-danger">{error}</div>}
      
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="systemName" className="form-label">System Name</label>
          <input
            type="text"
            className="form-control"
            id="systemName"
            name="systemName"
            value={asset.systemName}
            onChange={handleInputChange}
            required
          />
        </div>
        
        <div className="mb-3">
          <label htmlFor="ipAddress" className="form-label">IP Address</label>
          <input
            type="text"
            className="form-control"
            id="ipAddress"
            name="ipAddress"
            value={asset.ipAddress}
            onChange={handleInputChange}
            required
          />
        </div>
        
        <div className="mb-3">
          <label htmlFor="osVersion" className="form-label">OS Version</label>
          <input
            type="text"
            className="form-control"
            id="osVersion"
            name="osVersion"
            value={asset.osVersion}
            onChange={handleInputChange}
            required
          />
        </div>
        
        <div className="mb-3">
          <label htmlFor="systemTypeId" className="form-label">System Type</label>
          <select
            className="form-select"
            id="systemTypeId"
            name="systemTypeId"
            value={asset.systemTypeId}
            onChange={handleInputChange}
            required
          >
            <option value="">Select a system type</option>
            {systemTypes.map(type => (
              <option key={type.id} value={type.id}>
                {type.name}
              </option>
            ))}
          </select>
        </div>
        
        <div className="mb-3 form-check">
          <input
            type="checkbox"
            className="form-check-input"
            id="isActive"
            name="isActive"
            checked={asset.isActive}
            onChange={handleInputChange}
          />
          <label className="form-check-label" htmlFor="isActive">Active</label>
        </div>
        
        <div className="d-flex gap-2">
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Saving...' : 'Save Asset'}
          </button>
          <button
            type="button"
            className="btn btn-secondary"
            onClick={() => navigate(-1)}
            disabled={loading}
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
};