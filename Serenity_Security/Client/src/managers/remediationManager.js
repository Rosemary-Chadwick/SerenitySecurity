const baseUrl = "/api/remediation";

export const getRemediationById = (id) => {
  return fetch(`${baseUrl}/${id}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch remediation: ${text}`);
      });
    }
  });
};

export const toggleRemediationItem = (id, isCompleted) => {
  return fetch(`${baseUrl}/${id}/toggle`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ isCompleted }),
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to update remediation: ${text}`);
      });
    }
  });
};

export const createRemediationItem = (remediationData) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(remediationData),
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to create remediation: ${text}`);
      });
    }
  });
};

export const getRemediationsByVulnerabilityId = (vulnerabilityId) => {
  return fetch(`${baseUrl}/vulnerability/${vulnerabilityId}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      return res.text().then((text) => {
        throw new Error(`Failed to fetch remediations: ${text}`);
      });
    }
  });
};
