import { authHeader } from "../helpers";

const apiUrl = "http://localhost:44202/api";

async function login(email: string, password: string) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  };

  const response = await fetch(`${apiUrl}/users/authenticate`, requestOptions);
  const user = await handleResponse(response);
  if (user.token) {
    localStorage.setItem("user", JSON.stringify(user));
  }
  return user;
}

function logout() {
  localStorage.removeItem("user");
  localStorage.removeItem("Inventors");
}

async function register(user: any) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(user),
  };

  const response = await fetch(`${apiUrl}/users`, requestOptions);
  return handleResponse(response);
}

async function getAll() {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };

  const response = await fetch(`${apiUrl}/users`, requestOptions);
  return handleResponse(response);
}

async function getById(id: Number) {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };

  const response = await fetch(`${apiUrl}/users/${id}`, requestOptions);
  return handleResponse(response);
}

async function update(user: any) {
  const requestOptions = {
    method: "PUT",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(user),
  };

  const response = await fetch(`${apiUrl}/users/${user.id}`, requestOptions);
  return handleResponse(response);
}

async function deleteUser(id: any) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader(),
  };

  const response = await fetch(`${apiUrl}/users/${id}`, requestOptions);
  return handleResponse(response);
}

function handleResponse(response: any) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}

export const userService = {
  login,
  logout,
  register,
  getAll,
  getById,
  update,
  delete: deleteUser,
};
