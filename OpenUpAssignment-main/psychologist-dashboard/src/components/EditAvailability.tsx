import React from 'react';

const EditAvailability = () => {
    const availabilities = [
        'Monday 12:45 - 13:15',
        'Monday 12:45 - 13:15',
    ];

    return (
        <div>
            {availabilities.map((availability, index) => (
                <div key={index} style={{ display: 'flex', alignItems: 'center', marginBottom: '10px' }}>
                    <p style={{ margin: '0 10px 0 0' }}>{availability}</p>
                    <button style={{ marginRight: '5px', padding: '5px 10px' }}>Edit</button>
                </div>
            ))}
        </div>
    );
};

export default EditAvailability;
