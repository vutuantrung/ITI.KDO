import AuthService from '../services/AuthService';

async function checkErrors(res) {
  if (res.ok) return res;

  let errorMsg = `ERROR ${res.status} (${res.statusText})`;
  let serverText = await res.text();
  if (serverText) errorMsg = `${errorMsg}: ${serverText}`;

  let error = new Error(errorMsg);
  error.response = res;
  throw error;
}

function toJson(res) {
  return res.json();
}

export async function postAsync(url, data) {
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${AuthService.accessToken}`
    }
  })
    .then(checkErrors)
    .then(toJson);
}

export async function putAsync(url, data) {
  return await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${AuthService.accessToken}`
    }
  })
    .then(checkErrors)
    .then(toJson);
}

export async function getAsync(url) {
  return await fetch(url, {
    method: 'GET',
    headers: {
      Authorization: `Bearer ${AuthService.accessToken}`
    }
  })
    .then(checkErrors)
    .then(toJson);
}

export async function deleteAsync(url) {
  return await fetch(url, {
    method: 'DELETE',
    headers: {
      Authorization: `Bearer ${AuthService.accessToken}`
    }
  })
    .then(checkErrors)
    .then(toJson);
}
