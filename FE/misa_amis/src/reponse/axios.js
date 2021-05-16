import axios from 'axios';

export default axios.create({
    baseURL: 'https://localhost:44341/',
    timeout: 5000,
    headers: {
        'Content-Type': 'application/json'
    },
});