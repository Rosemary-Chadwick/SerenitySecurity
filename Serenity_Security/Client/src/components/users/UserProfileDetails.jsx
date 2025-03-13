import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { getCurrentUserProfile, deleteUserProfile } from "../../managers/userProfileManager";
import { Button, Card, CardBody, CardHeader, Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";

export const UserProfileDetails = ({ setLoggedInUser }) => {
  const [userProfile, setUserProfile] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    getCurrentUserProfile()
      .then((data) => {
        setUserProfile(data);
        setIsLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setIsLoading(false);
      });
  }, []);

  const handleDeleteClick = () => {
    setShowDeleteModal(true);
  };

  const handleDeleteConfirm = () => {
    setIsLoading(true);
    deleteUserProfile(userProfile.id)
      .then(() => {
        // Log out the user after successful deletion
        localStorage.removeItem("userProfile");
        setLoggedInUser(null);
        navigate("/login");
      })
      .catch((err) => {
        setError(`Failed to delete profile: ${err.message}`);
        setIsLoading(false);
        setShowDeleteModal(false);
      });
  };

  const toggleDeleteModal = () => {
    setShowDeleteModal(!showDeleteModal);
  };

  if (isLoading) {
    return <div>Loading profile information...</div>;
  }

  if (error) {
    return <div className="alert alert-danger">Error: {error}</div>;
  }

  if (!userProfile) {
    return <div>Profile not found.</div>;
  }

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>My Profile</h2>
        <Button
          onClick={() => navigate("/")}
          color="secondary"
        >
          Back to Dashboard
        </Button>
      </div>

      <Card className="mb-4">
        <CardHeader>
          <h4>Profile Information</h4>
        </CardHeader>
        <CardBody>
          <p><strong>Username:</strong> {userProfile.username}</p>
          <p><strong>Email:</strong> {userProfile.email}</p>
          {userProfile.isAdmin !== null && (
            <p><strong>Role:</strong> {userProfile.isAdmin ? "Administrator" : "Standard User"}</p>
          )}
          <div className="mt-3">
            <Link to="/profile/edit" className="btn btn-primary me-2">
              Edit Profile
            </Link>
            <Button color="danger" onClick={handleDeleteClick}>
              Delete Account
            </Button>
          </div>
        </CardBody>
      </Card>

      {/* Delete Confirmation Modal */}
      <Modal isOpen={showDeleteModal} toggle={toggleDeleteModal}>
        <ModalHeader toggle={toggleDeleteModal}>Confirm Account Deletion</ModalHeader>
        <ModalBody>
          <div className="alert alert-danger">
            <p><strong>Warning:</strong> This action cannot be undone.</p>
            <p>Deleting your account will also delete:</p>
            <ul>
              <li>All your assets</li>
              <li>All reports associated with your assets</li>
              <li>All vulnerability data associated with your reports</li>
            </ul>
            <p>Are you sure you want to delete your account?</p>
          </div>
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggleDeleteModal}>
            Cancel
          </Button>
          <Button color="danger" onClick={handleDeleteConfirm}>
            Delete Account
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};