import axiosInstance from './axiosInstance'; 

export const fetchData = async () => {
    try {
        const response = await axiosInstance.get('sessions');
        return response.data;
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
};

export const postAvailability = async (userId, from, to) => {
    try {
        const response = await axiosInstance.post(`Psychologist/${userId}/available-timeslots`, {
            from,
            to,
        });
        return response.data;
    } catch (error) {
        console.error('Error posting availability:', error);
        throw error;
    }
};

export const getPsychologistDetails = async (psychologistId) => {
    try {
        const response = await axiosInstance.get(`Psychologist/${psychologistId}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching psychologist details:', error);
        throw error;
    }
};

export const patchAvailability = async (psychologistId, availabilityId, from, to) => {
    try {
        const response = await axiosInstance.patch(
            `Psychologist/${psychologistId}/available-timeslots/${availabilityId}`,
            { from, to }
        );
        return response.data;
    } catch (error) {
        console.error('Error updating availability:', error);
        throw error;
    }
};

export const deleteAvailability = async (psychologistId, availabilityId) => {
    try {
        await axiosInstance.delete(`Psychologist/${psychologistId}/available-timeslots/${availabilityId}`);
        return true;
    } catch (error) {
        console.error('Error deleting availability:', error);
        throw error;
    }
};

export const getClientDetails = async (clientId) => {
    try {
        const response = await axiosInstance.get(`Client/${clientId}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching client details:', error);
        throw error;
    }
};

export const deleteBooking = async (clientId, bookingId) => {
    try {
        const response = await axiosInstance.delete(`Client/${clientId}/bookings/${bookingId}`);
        return response.data;
    } catch (error) {
        console.error('Error deleting booking:', error);
        throw error;
    }
};

export const fetchPsychologistIds = async () => {
    try {
        const response = await axiosInstance.get('Psychologist/ids');
        return response.data;
    } catch (error) {
        console.error('Error fetching psychologist IDs:', error);
        throw error;
    }
};
