import React from 'react';
import AddAvailability from './components/AddAvailability';
import EditAvailability from './components/EditAvailability';
import UpcomingSessions from './components/UpcomingSessions';

function App() {
    return (
        <div style={{ fontFamily: 'Arial, sans-serif', padding: '20px' }}>
            <h1 style={{ textAlign: 'center' }}>Psychologist Dashboard</h1>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
                <div style={{ width: '48%', border: '1px solid black', padding: '10px', borderRadius: '5px' }}>
                    <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}>Add Availability</h3>
                    <AddAvailability />
                </div>
                <div style={{ width: '48%', border: '1px solid black', padding: '10px', borderRadius: '5px' }}>
                    <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}>Edit Availability</h3>
                    <EditAvailability />
                </div>
            </div>
            <div style={{ border: '1px solid black', padding: '10px', borderRadius: '5px' }}>
                <h3 style={{ margin: '0 0 10px 0', borderBottom: '1px solid black' }}>Upcoming Sessions</h3>
                <UpcomingSessions />
            </div>
        </div>
    );
}

export default App;
