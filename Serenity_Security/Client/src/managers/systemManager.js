const apiUrl = "/api/systemtypes";

export const getAllSystemTypes = () => {
  return fetch(apiUrl)
    .then((res) => res.json())
    .then((data) => {
      return data;
    });
};
