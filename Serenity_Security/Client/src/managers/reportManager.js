export const getReportsByAssetId = (assetId) => {
  return fetch(`/api/report/asset/${assetId}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error("An error occurred while fetching the reports");
    }
  });
};
export const getReportById = (id) => {
  return fetch(`/api/report/${id}`).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error("An error occurred while fetching the report");
    }
  });
};
