import React, { useState } from 'react';
import { postAvailability } from '../services/api';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import "../styles/styles.css";  

const AddAvailability = ({ userId, onAvailabilityAdded }) => {
    const [fromDate, setFromDate] = useState('');
    const [toDate, setToDate] = useState('');
    const [fromTime, setFromTime] = useState('');
    const [toTime, setToTime] = useState('');
    const [loading, setLoading] = useState(false);

    const formatToLocalISOString = (date, time) => {
        // Combine date and time, and return as local ISO string
        return `${date}T${time}:00`;
    };

    const handleSubmit = async () => {
        if (!fromDate || !toDate || !fromTime || !toTime) {
            toast.error('Please fill all fields');
            return;
        }

        const from = formatToLocalISOString(fromDate, fromTime);
        const to = formatToLocalISOString(toDate, toTime);
        const diffHours = (new Date(to).getTime() - new Date(from).getTime()) / (1000 * 60 * 60);
        if (new Date(from) >= new Date(to)) {
            toast.error('"From" date and time must be earlier than "To" date and time');
            return;
        }
        if (diffHours > 2) {
            toast.error('Invalid interval: Time difference cannot exceed 2 hours');
            return;
        }
        setLoading(true);

        try {
            const result = await postAvailability(userId, from, to); // Send local ISO
            toast.success('Availability added successfully!');
            onAvailabilityAdded({ ...result, from, to }); 
        } catch (error) {
            toast.error('Failed to add availability');
        } finally {
            setLoading(false);
        }
    };
    return (
        <div className="container">
            <div className="input-group">
                <div className="input-item">
                    <label>
                        From Date
                        <input
                            type="date"
                            value={fromDate}
                            onChange={(e) => setFromDate(e.target.value)}
                            className="input"
                        />
                    </label>
                    <label>
                        From Time
                        <input
                            type="time"
                            value={fromTime}
                            onChange={(e) => setFromTime(e.target.value)}
                            className="input"
                        />
                    </label>
                </div>
                <div className="input-item">
                    <label>
                        To Date
                        <input
                            type="date"
                            value={toDate}
                            onChange={(e) => setToDate(e.target.value)}
                            className="input"
                        />
                    </label>
                    <label>
                        To Time
                        <input
                            type="time"
                            value={toTime}
                            onChange={(e) => setToTime(e.target.value)}
                            className="input"
                        />
                    </label>
                </div>
            </div>
            <button
                className="button"
                onClick={handleSubmit}
                disabled={loading}
            >
                {loading ? 'Adding...' : 'Add'}
            </button>
        </div>
    );
};

export default AddAvailability;
