import AuthService from '../services/AuthService'
import $ from 'jquery'

function dataFilter(data, type) {
    if(data === '') return null;
    return data;
}


async function checkErrors(resp) {
    if(resp.ok) return resp;

    let errorMsg = `ERROR ${resp.status} (${resp.statusText})`;
    let serverText = await resp.text();
    if(serverText) errorMsg = `${errorMsg}: ${serverText}`;

    var error = new Error(errorMsg);
    error.response = resp;
    throw error;
}

function toJSON(resp) {
    return resp.json();
}

export async function postAsync(url, data) {
    return await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function putAsync(url, data) {
    return await fetch(url, {
        method: 'PUT',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function getAsync(url) {
    return await fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function deleteAsync(url) {
    return await fetch(url, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}


export async function postTypeAsync(url) {
    return await fetch(url, {
        method: 'POST',
        headers: {
            'Authorization' : `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function postFileAsync(url, data) {
    console.log("votre data WIIN : " + data);
    console.dir(data);  
      return await $.ajax({
        method: 'POST',
        url: url,
        contentType: false,
        cache: false,
        data: data,
        dataFilter: dataFilter,
        processData: false,
        headers: {
            Authorization: `Bearer ${AuthService.accessToken}`
        }
    });
}