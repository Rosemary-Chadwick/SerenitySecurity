export const getUserAssets = () => {
  return fetch("/api/asset", {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else if (res.status === 401) {
      throw new Error("Unauthorized");
    } else {
      throw new Error("Failed to fetch assets");
    }
  });
};
export const getAssetById = (id) => {
  return fetch(`/api/asset/${id}`, {
    method: "GET",
    credentials: "same-origin", // This ensures cookies are sent with the request
    headers: {
      "Content-Type": "application/json",
    },
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else if (res.status === 404) {
      throw new Error("Asset not found");
    } else {
      throw new Error("Failed to fetch asset");
    }
  });
};
