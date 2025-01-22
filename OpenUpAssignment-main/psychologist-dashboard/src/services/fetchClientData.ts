import axios from 'axios';

export const getClientData = async () => {
    try {
        const response = await axios.get('https://localhost:44301/clients');
        return response.data;
    } catch (error) {
        console.error('Error fetching client data', error);
        return [];
    }
};
