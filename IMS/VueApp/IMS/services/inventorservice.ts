import { authHeader } from "../helpers";

const apiUrl = "http://localhost:44202/api";

async function create(inventory: any) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(inventory),
  };

  const response = await fetch(`${apiUrl}/inventories`, requestOptions);
  return handleResponse(response);
}

async function getInventoriesByUserId(id: Number) {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };

  const response = await fetch(
    `${apiUrl}/inventories/getbyUserId/${id}`,
    requestOptions
  );
  return handleResponse(response);
}

function handleResponse(response: any) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}

export const inventoryService = {
  getInventoriesByUserId,
  create,
};
