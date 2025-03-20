const baseUrl = "/api/dashboard";

// Function to get vulnerability counts by severity
export const getVulnerabilitySeverityStats = () => {
  return fetch(`${baseUrl}/vulnerability-severity`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch vulnerability statistics: ${text}`);
      });
    }
  });
};

// Function to get remediation status counts
export const getRemediationStatusStats = () => {
  return fetch(`${baseUrl}/remediation-status`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch remediation statistics: ${text}`);
      });
    }
  });
};

// Function to get vulnerability counts by asset
export const getAssetVulnerabilityStats = () => {
  return fetch(`${baseUrl}/asset-vulnerabilities`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(
          `Failed to fetch asset vulnerability statistics: ${text}`
        );
      });
    }
  });
};

// Function to get user assets (using the existing endpoint)
export const getUserAssets = () => {
  return fetch("/api/asset").then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch assets: ${text}`);
      });
    }
  });
};

// Function to get a vulnerability by ID
export const getVulnerabilityById = (id) => {
  return fetch(`/api/vulnerability/${id}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch vulnerability: ${text}`);
      });
    }
  });
};

// Function to get a remediation by ID
export const getRemediationById = (id) => {
  return fetch(`/api/remediation/${id}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch remediation: ${text}`);
      });
    }
  });
};
