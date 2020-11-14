import axios from 'axios';

export default axios.create({
    baseURL:"https://localhost:44323",
    headers:{
        "Content-type":"application/json; charset=utf-8",
        "Accept": "application/json, text/plain",
    }
})