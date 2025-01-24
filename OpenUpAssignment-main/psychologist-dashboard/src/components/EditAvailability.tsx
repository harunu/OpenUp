import React, { useEffect, useState } from 'react';
import { getPsychologistDetails, patchAvailability, deleteAvailability } from '../services/api';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import "../styles/app.css";

const EditAvailability = ({ psychologistId, availabilities: externalAvailabilities, onDataSync }) => {
    const [availabilities, setAvailabilities] = useState([]);
    const [editingId, setEditingId] = useState(null);
    const [editData, setEditData] = useState({ from: '', to: '' });

    // Adjust UTC to local time
    const adjustToLocalTime = (utcString) => {
        const date = new Date(utcString);
        const tzOffset = date.getTimezoneOffset() * 60000;
        return new Date(date.getTime() - tzOffset).toISOString().slice(0, 16);
    };
    const formatForDisplay = (utcString) => {
        const date = new Date(utcString);
        return date.toLocaleString([], {
            dateStyle: 'medium',
            timeStyle: 'short',
        }).trim();;
    };
    // Fetch data from the API and adjust timezone
    const fetchAvailabilities = async () => {
        try {
            const response = await getPsychologistDetails(psychologistId);
            const updatedAvailabilities = response.availableTimeSlots.map((slot) => ({
                ...slot,
                from: adjustToLocalTime(slot.from),
                to: adjustToLocalTime(slot.to),
            }));
            setAvailabilities(updatedAvailabilities);
            onDataSync(updatedAvailabilities);
        } catch (error) {
            console.error('Error fetching availabilities:', error);
            toast.error('Failed to fetch updated availabilities.');
        }
    };

    // Initialize availabilities 
    useEffect(() => {
        if (externalAvailabilities) {
            setAvailabilities(
                externalAvailabilities.map((slot) => ({
                    ...slot,
                    from: adjustToLocalTime(slot.from),
                    to: adjustToLocalTime(slot.to),
                }))
            );
        }
    }, [externalAvailabilities]);

    // Trigger fetchAvailabilities when new records are added
    useEffect(() => {
        fetchAvailabilities();
    }, [availabilities.length]); // Check for length changes in the state

    const handleEditClick = (availability) => {
        setEditingId(availability.id);
        setEditData({
            from: availability.from,
            to: availability.to,
        });
    };

    const handleCancelEdit = () => {
        setEditingId(null);
        setEditData({ from: '', to: '' });
    };

    // Validate input before saving
    const validateInput = () => {
        const fromDate = new Date(editData.from);
        const toDate = new Date(editData.to);
        if (!editData.from || !editData.to) {
            toast.error('Please fill all fields');
            return false;
        }

        if (fromDate >= toDate) {
            toast.error('"Start" date and time must be earlier than "End" date and time');
            return;
        }

        if (fromDate >= toDate) {
            toast.error('Invalid interval: "Start Time" must be earlier than "End Time"');
            return false;
        }

        const diffHours = (toDate.getTime() - fromDate.getTime()) / (1000 * 60 * 60);
        if (diffHours > 2) {
            toast.error('Invalid interval: Time difference cannot exceed 2 hours');
            return false;
        }

        return true;
    };

    const handleSaveEdit = async () => {
        if (!validateInput()) return;

        try {
            const { from, to } = editData;

            await patchAvailability(psychologistId, editingId, from, to);
            await fetchAvailabilities();
            toast.success('Availability updated successfully!');
            handleCancelEdit();
        } catch (error) {
            console.error('Error updating availability:', error);
            toast.error('Failed to update availability.');
        }
    };

    const handleDelete = async (availabilityId) => {
        try {
            await deleteAvailability(psychologistId, availabilityId);
            await fetchAvailabilities();
            toast.success('Availability deleted successfully!');
        } catch (error) {
            console.error('Error deleting availability:', error);
            toast.error('Failed to delete availability.');
        }
    };

    return (
        <div>
            {availabilities.length === 0 ? (
                <div className="no-availability">
                    <p>No availabilities found.</p>
                </div>) : (
                availabilities.map((availability) => (
                    <div
                        key={availability.id}
                        style={{
                            display: 'flex',
                            flexDirection: 'column',
                            marginBottom: '10px',
                            border: '1px solid #ccc',
                            padding: '10px',
                            borderRadius: '5px',
                        }}
                    >
                        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                            {/* <p style={{ margin: 0 }}>
                                <b>From:</b> {availability.from}
                                <b> To:</b> {availability.to}
                            </p> */}
                            <p style={{ margin: 0 }}>
                                <b>From:</b> {formatForDisplay(availability.from)} <br />
                                <b>To:</b> {formatForDisplay(availability.to)}
                            </p>
                            <div>
                                <button
                                    style={{ marginRight: '5px', padding: '5px 10px' }}
                                    onClick={() => handleEditClick(availability)}
                                >
                                    Edit
                                </button>
                                <button
                                    style={{
                                        padding: '5px 10px',
                                        backgroundColor: 'red',
                                        color: 'white',
                                    }}
                                    onClick={() => handleDelete(availability.id)}
                                >
                                    Delete
                                </button>
                            </div>
                        </div>

                        {editingId === availability.id && (
                            <div
                                style={{
                                    marginTop: '10px',
                                    padding: '10px',
                                    border: '1px solid #ddd',
                                    borderRadius: '5px',
                                    backgroundColor: '#f9f9f9',
                                }}
                            >
                                <div style={{ display: 'flex', gap: '20px', alignItems: 'center' }}>
                                    <label>
                                        From:
                                        <input
                                            type="datetime-local"
                                            value={editData.from}
                                            onChange={(e) =>
                                                setEditData({ ...editData, from: e.target.value })
                                            }
                                            style={{ marginLeft: '10px' }}
                                        />
                                    </label>
                                    <label>
                                        To:
                                        <input
                                            type="datetime-local"
                                            value={editData.to}
                                            onChange={(e) =>
                                                setEditData({ ...editData, to: e.target.value })
                                            }
                                            style={{ marginLeft: '10px' }}
                                        />
                                    </label>
                                </div>
                                <div style={{ marginTop: '10px', display: 'flex', gap: '10px' }}>
                                    <button
                                        style={{ padding: '5px 10px', backgroundColor: '#007bff', color: 'white' }}
                                        onClick={handleSaveEdit}
                                    >
                                        Save
                                    </button>
                                    <button
                                        style={{
                                            padding: '5px 10px',
                                            backgroundColor: 'gray',
                                            color: 'white',
                                        }}
                                        onClick={handleCancelEdit}
                                    >
                                        Cancel
                                    </button>
                                </div>
                            </div>
                        )}
                    </div>
                ))
            )}
        </div>
    );
};

export default EditAvailability;

