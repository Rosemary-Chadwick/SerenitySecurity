const _apiUrl = "/api/systemtypes";

export const getAllSystemTypes = () => {
  return fetch(_apiUrl, {
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
      throw new Error("Failed to fetch system types");
    }
  });
};
