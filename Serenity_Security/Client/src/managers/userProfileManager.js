const _apiUrl = "/api/userprofile";

export const getCurrentUserProfile = () => {
  return fetch(`${_apiUrl}/current`).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to get user profile");
    }
    return res.json();
  });
};

export const getUserProfileById = (id) => {
  return fetch(`${_apiUrl}/${id}`).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to get user profile");
    }
    return res.json();
  });
};

export const updateUserProfile = (profile) => {
  return fetch(`${_apiUrl}/${profile.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: profile.username,
      email: profile.email,
    }),
  }).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to update profile");
    }
    return res;
  });
};

export const deleteUserProfile = (id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "DELETE",
  }).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to delete profile");
    }
    return res;
  });
};
