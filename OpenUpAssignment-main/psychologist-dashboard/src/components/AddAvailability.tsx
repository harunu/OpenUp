import React, { useState } from 'react';

const AddAvailability = () => {
    const [fromDate, setFromDate] = useState('');
    const [toDate, setToDate] = useState('');
    const [fromTime, setFromTime] = useState('');
    const [toTime, setToTime] = useState('');

    const handleSubmit = () => {
        console.log('Availability added:', { fromDate, toDate, fromTime, toTime });
    };

    return (
        <div>
            <div style={{ display: 'flex', marginBottom: '10px' }}>
                <div style={{ marginRight: '10px' }}>
                    <label>From</label>
                    <input
                        type="date"
                        value={fromDate}
                        onChange={(e) => setFromDate(e.target.value)}
                        style={{ margin: '5px 0', display: 'block' }}
                    />
                    <input
                        type="time"
                        value={fromTime}
                        onChange={(e) => setFromTime(e.target.value)}
                        style={{ margin: '5px 0', display: 'block' }}
                    />
                </div>
                <div>
                    <label>To</label>
                    <input
                        type="date"
                        value={toDate}
                        onChange={(e) => setToDate(e.target.value)}
                        style={{ margin: '5px 0', display: 'block' }}
                    />
                    <input
                        type="time"
                        value={toTime}
                        onChange={(e) => setToTime(e.target.value)}
                        style={{ margin: '5px 0', display: 'block' }}
                    />
                </div>
            </div>
            <button onClick={handleSubmit} style={{ padding: '5px 10px' }}>Add</button>
        </div>
    );
};

export default AddAvailability;
