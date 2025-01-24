import React, { useState, useEffect } from 'react';
import AddAvailability from './components/AddAvailability';
import EditAvailability from './components/EditAvailability';
import UpcomingSessions from './components/UpcomingSessions';
import { getPsychologistDetails, fetchPsychologistIds } from './services/api';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import "./styles/app.css";  

function App() {
    const [psychologistId, setPsychologistId] = useState('');
    const [availabilities, setAvailabilities] = useState([]);
    const [submitted, setSubmitted] = useState(false);
    const [psychologistIds, setPsychologistIds] = useState([]);

    useEffect(() => {
        const fetchIds = async () => {
            try {
                const ids = await fetchPsychologistIds();
                setPsychologistIds(ids);
            } catch (error) {
                console.error('Error fetching psychologist IDs:', error);
            }
        };

        fetchIds();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!psychologistId) return;

        setSubmitted(true);

        try {
            const data = await getPsychologistDetails(psychologistId);
            const processedAvailabilities = data.availableTimeSlots.map((slot) => ({
                ...slot,
                from: new Date(slot.from),
                to: new Date(slot.to),
            }));
            setAvailabilities(processedAvailabilities);
        } catch (error) {
            alert('Failed to fetch psychologist data');
            console.error(error);
        }
    };

    const handleAvailabilityAdded = (newAvailability) => {
        const processedAvailability = {
            ...newAvailability,
            from: new Date(newAvailability.from),
            to: new Date(newAvailability.to),
        };

        setAvailabilities((prev) => [...prev, processedAvailability]);
    };

    return (
        <div style={{ fontFamily: 'Arial, sans-serif', padding: '20px' }}>
            <ToastContainer />
            <h1 style={{ textAlign: 'center' }}>Psychologist Dashboard</h1>

            {!submitted ? (
                <form onSubmit={handleSubmit} className="psychologist-select">
                    <select
                        value={psychologistId}
                        onChange={(e) => setPsychologistId(e.target.value)}
                        required
                        style={{
                            padding: '10px',
                            fontSize: '16px',
                            width: '220px',
                            marginRight: '10px',
                        }}
                    >
                        <option value="" disabled>
                            Select Psychologist ID
                        </option>
                        {psychologistIds.map((id) => (
                            <option key={id} value={id}>
                                {id}
                            </option>
                        ))}
                    </select>
                    <button
                        type="submit"
                        style={{ padding: '10px 20px', fontSize: '16px' }}
                    >
                        Submit
                    </button>
                </form>
            ) : (
                <>
                    <div style={{ display: 'flex', gap: '20px', marginBottom: '20px' }}>
                        <div className="availability-card">
                            <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}>Add Availability</h3>
                            <AddAvailability
                                userId={psychologistId}
                                onAvailabilityAdded={handleAvailabilityAdded}
                            />
                        </div>
                        <div className="availability-card">
                            <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}>Edit Availability</h3>
                            <EditAvailability
                                psychologistId={psychologistId}
                                availabilities={availabilities}
                                onDataSync={setAvailabilities}
                            />
                        </div>
                    </div>

                    <div className="upcoming-sessions">
                        <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}></h3>
                        <UpcomingSessions psychologistId={psychologistId} />
                    </div>
                </>
            )}
        </div>
    );
}

export default App;