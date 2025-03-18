import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createAsset, getAssetById, updateAsset } from '../../managers/assetManager';
import { getAllSystemTypes } from '../../managers/systemTypeManager';
import { useTheme } from '../theme/ThemeContext'; 
import { Button, Card, CardBody, CardHeader, CardTitle, FormGroup, Label, Input } from 'reactstrap';

export const AssetForm = () => {
  // Get the id parameter from the URL if it exists
  const { id } = useParams();
  const navigate = useNavigate();
  const { colors } = useTheme();
  
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
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-7">
          <Card className="mb-4 shadow">
          <CardHeader style={{ 
              backgroundColor: colors.primary,
              color: colors.secondary,
              borderTopLeftRadius: 'inherit',
              borderTopRightRadius: 'inherit' 
            }}>
              <CardTitle tag="h5" className="mb-0 text-center">
                {isEditMode ? 'Update Asset' : 'Create New Asset'}
              </CardTitle>
            </CardHeader>
            
            <CardBody style={{ 
              backgroundColor: colors.cardBg,
              color: colors.cardText 
            }}>
              {error && <div className="alert alert-danger">{error}</div>}
              
              <form onSubmit={handleSubmit}>
                <FormGroup>
                  <Label htmlFor="systemName">System Name</Label>
                  <Input
                    type="text"
                    id="systemName"
                    name="systemName"
                    value={asset.systemName}
                    onChange={handleInputChange}
                    required
                    style={{
                      backgroundColor: 'transparent',
                      color: colors.cardText,
                      borderColor: colors.cardText
                    }}
                  />
                </FormGroup>
                
                <FormGroup>
                  <Label htmlFor="ipAddress">IP Address</Label>
                  <Input
                    type="text"
                    id="ipAddress"
                    name="ipAddress"
                    value={asset.ipAddress}
                    onChange={handleInputChange}
                    required
                    style={{
                      backgroundColor: 'transparent',
                      color: colors.cardText,
                      borderColor: colors.cardText
                    }}
                  />
                </FormGroup>
                
                <FormGroup>
                  <Label htmlFor="osVersion">OS Version</Label>
                  <Input
                    type="text"
                    id="osVersion"
                    name="osVersion"
                    value={asset.osVersion}
                    onChange={handleInputChange}
                    required
                    style={{
                      backgroundColor: 'transparent',
                      color: colors.cardText,
                      borderColor: colors.cardText
                    }}
                  />
                </FormGroup>
                
                <FormGroup>
                  <Label htmlFor="systemTypeId">System Type</Label>
                  <Input
                    type="select"
                    id="systemTypeId"
                    name="systemTypeId"
                    value={asset.systemTypeId}
                    onChange={handleInputChange}
                    required
                    style={{
                      backgroundColor: 'transparent',
                      color: colors.cardText,
                      borderColor: colors.cardText
                    }}
                  >
                    <option value="">Select a system type</option>
                    {systemTypes.map(type => (
                      <option key={type.id} value={type.id}>
                        {type.name}
                      </option>
                    ))}
                  </Input>
                </FormGroup>
                
                <FormGroup check className="mb-3">
                  <Label check>
                    <Input
                      type="checkbox"
                      name="isActive"
                      checked={asset.isActive}
                      onChange={handleInputChange}
                    />
                    Active
                  </Label>
                </FormGroup>
                
                <div className="d-flex gap-2 justify-content-between mt-4">
                  <Button type="submit" disabled={loading} style={{
                    backgroundColor: colors.buttonHighlight,
                    color: colors.primary,
                    border: 'none'
                  }}>
                    {loading ? 'Saving...' : 'Save Asset'}
                  </Button>
                  
                  <Button type="button" onClick={() => navigate(-1)} disabled={loading} style={{
                    backgroundColor: 'transparent',
                    color: colors.cardText,
                    border: `1px solid ${colors.cardText}`
                  }}>
                    Cancel
                  </Button>
                </div>
              </form>
            </CardBody>
          </Card>
        </div>
      </div>
    </div>
  );
};