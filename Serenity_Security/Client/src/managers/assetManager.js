const _apiUrl = "/api/asset"; // assets?

export const getUserAssets = () => {
  return fetch(_apiUrl, {
    method: "GET",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error("An error occurred while fetching the assets");
    }
  });
};
export const getAssetById = (id) => {
  return fetch(_apiUrl + `/${id}`, {
    method: "GET",
    credentials: "same-origin", // This ensures cookies are sent with the request
    headers: {
      "Content-Type": "application/json",
    },
  }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error("An error occurred while fetching the asset");
    }
  });
};
export const createAsset = (asset) => {
  return fetch(_apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(asset),
  }).then((res) => res.json());
};
export const updateAsset = (id, asset) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(asset),
  });
};
export const deleteAsset = (id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "DELETE",
  });
};
