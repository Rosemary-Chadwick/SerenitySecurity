// src/App.jsx
import { useState, useEffect } from "react";
import ApplicationViews from "./components/ApplicationViews";
import { NavBar } from "./components/NavBar";
import { tryGetLoggedInUser } from "./managers/authManager";
import { ThemeProvider } from "./components/theme/ThemeContext";
import 'bootstrap/dist/css/bootstrap.min.css';
import './custom.css';

function App() {
  const [loggedInUser, setLoggedInUser] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    // Try to get logged in user from the server
    tryGetLoggedInUser().then((user) => {
      setLoggedInUser(user);
      setIsLoading(false);
    });
  }, []);

  // Show loading indicator while checking authentication
  if (isLoading) {
    return (
      <div className="d-flex justify-content-center align-items-center" style={{ height: "100vh" }}>
        <div className="spinner-border text-primary" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    );
  }

  return (
    <ThemeProvider>
      <NavBar loggedInUser={loggedInUser} setLoggedInUser={setLoggedInUser} />
      <ApplicationViews
        loggedInUser={loggedInUser}
        setLoggedInUser={setLoggedInUser}
      />
    </ThemeProvider>
  );
}

export default App;