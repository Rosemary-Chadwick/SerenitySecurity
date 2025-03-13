import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getCurrentUserProfile, updateUserProfile } from "../../managers/userProfileManager";
import { Button, Form, FormGroup, Input, Label, Alert } from "reactstrap";

export const UserProfileEdit = () => {
  const [userProfile, setUserProfile] = useState({
    id: 0,
    username: "",
    email: "",
  });
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [formError, setFormError] = useState(null);
  const [updateSuccess, setUpdateSuccess] = useState(false);
  
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    getCurrentUserProfile()
      .then((data) => {
        setUserProfile({
          id: data.id,
          username: data.username || data.userName || data.Username || "",
          email: data.email || data.Email || "",
        });
        setIsLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setIsLoading(false);
      });
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUserProfile((prevProfile) => ({
      ...prevProfile,
      [name]: value,
    }));
    setFormError(null);
    setUpdateSuccess(false);
  };

  const validateForm = () => {
    if (!userProfile.username.trim()) {
      setFormError("Username is required");
      return false;
    }
    
    if (!userProfile.email.trim()) {
      setFormError("Email is required");
      return false;
    }
    
    // Simple email validation
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(userProfile.email)) {
      setFormError("Please enter a valid email address");
      return false;
    }
    
    return true;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }
    
    setIsLoading(true);
    updateUserProfile(userProfile)
      .then(() => {
        setUpdateSuccess(true);
        setIsLoading(false);
      })
      .catch((err) => {
        setFormError(`Failed to update profile: ${err.message}`);
        setIsLoading(false);
      });
  };

  if (isLoading && !userProfile.id) {
    return <div>Loading profile information...</div>;
  }

  if (error) {
    return <div className="alert alert-danger">Error: {error}</div>;
  }

  return (
    <div className="container mt-4" style={{ maxWidth: "600px" }}>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Edit Profile</h2>
        <Button
          onClick={() => navigate("/profile")}
          color="secondary"
        >
          Back to Profile
        </Button>
      </div>

      {formError && <Alert color="danger">{formError}</Alert>}
      {updateSuccess && <Alert color="success">Profile updated successfully!</Alert>}

      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="username">Username</Label>
          <Input
            type="text"
            id="username"
            name="username"
            value={userProfile.username}
            onChange={handleInputChange}
            required
          />
        </FormGroup>
        
        <FormGroup>
          <Label for="email">Email</Label>
          <Input
            type="email"
            id="email"
            name="email"
            value={userProfile.email}
            onChange={handleInputChange}
            required
          />
        </FormGroup>

        <div className="d-flex mt-4">
          <Button 
            type="submit" 
            color="primary" 
            className="me-2"
            disabled={isLoading}
          >
            {isLoading ? "Saving..." : "Save Changes"}
          </Button>
          <Button 
            type="button" 
            color="secondary" 
            onClick={() => navigate("/profile")}
          >
            Cancel
          </Button>
        </div>
      </Form>
    </div>
  );
};