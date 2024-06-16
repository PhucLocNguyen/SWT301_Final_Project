const axiosConfigHeader = {
    headers: {
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("userInfo")!= null? localStorage.getItem("userInfo") : ""
    },
    withCredentials: true // Nếu API của bạn yêu cầu cookie
};
export default axiosConfigHeader;