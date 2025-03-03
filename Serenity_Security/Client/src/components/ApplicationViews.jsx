import { Routes, Route } from "react-router-dom";
import Login  from "./auth/Login";
import Register from "./auth/Register";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import { AssetList } from "./assets/AssetList"; // Import the new component
import { AssetDetails } from "./assets/AssetDetails";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
             <h1>Welcome to Serenity Security</h1>
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