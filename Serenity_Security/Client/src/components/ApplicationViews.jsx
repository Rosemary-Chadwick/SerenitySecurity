import { Routes, Route } from "react-router-dom";
import Login  from "./auth/Login";
import Register from "./auth/Register";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import { AssetList } from "./assets/AssetList"; // Import the new component
import { AssetDetails } from "./assets/AssetDetails";
import { AssetForm } from "./assets/AssetCreateUpdateForm";
import { ReportDetails } from "./reports/ReportDetails";
import { VulnerabilityDetails } from "./vulnerabilities/VulnerabilityDetails";
import { RemediationDetails } from "./remediations/RemediationDetails";
import { UserProfileDetails } from "./users/UserProfileDetails";
import { UserProfileEdit } from "./users/UserProfileEdit";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
             {/* <h1>Welcome to Serenity Security</h1> */}
              <AssetList />
            </AuthorizedRoute>
          }
        />
        
        <Route
          path="assets"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <AssetList />
            </AuthorizedRoute>
          }
        />
        <Route
          path="assets/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <AssetDetails />
            </AuthorizedRoute>
          }
        />
        <Route
          path="assets/create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <AssetForm />
            </AuthorizedRoute>
          }
        />
                <Route
          path="assets/edit/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <AssetForm />
            </AuthorizedRoute>
          }
        />
                <Route
          path="report/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <ReportDetails />
            </AuthorizedRoute>
          }
        />
        
        <Route
          path="vulnerability/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <VulnerabilityDetails />
            </AuthorizedRoute>
          }
        />
        <Route
          path="remediation/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <RemediationDetails />
            </AuthorizedRoute>
          }
          />
                  <Route
          path="profile"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <UserProfileDetails setLoggedInUser={setLoggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route
          path="profile/edit"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <UserProfileEdit />
            </AuthorizedRoute>
          }
        />
        
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here... This page doesn't exist yet</p>} />
    </Routes>
  );
}