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
    } else {
      throw new Error("An error occurred while fetching the system types");
    }
  });
};
