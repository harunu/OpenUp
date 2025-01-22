export const fetchData = () => {
    return fetch('https://api.fakebackend.com/sessions').then((res) => res.json());
};
