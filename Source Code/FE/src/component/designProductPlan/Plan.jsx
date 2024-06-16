function Plan({ onClick }) {
    return (
      <div onClick={onClick} className="cursor-pointer">
        <div
          className="relative flex items-center mt-3 h-28 bg-white rounded-lg cursor-pointer bg-opacity-90 group hover:bg-opacity-100 drop-shadow-lg"
          draggable="true"
        >
          <div className="w-2/5">
            <div className="absolute top-0 right-0 flex items-center justify-center hidden w-5 h-5 mt-3 mr-2 text-gray-500 rounded hover:bg-gray-200 hover:text-gray-700 group-hover:flex">
              <svg
                className="w-4 h-4 fill-current"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path d="M10 6a2 2 0 110-4 2 2 0 010 4zM10 12a2 2 0 110-4 2 2 0 010 4zM10 18a2 2 0 110-4 2 2 0 010 4z" />
              </svg>
            </div>
            <span className="flex items-center px-3 text-xs font-semibold text-green-500 bg-green-100 rounded-full h-fit w-fit py-1 ml-3">
              ID0001
            </span>
            <h4 className="mt-4 text-sm font-medium bg-[#4338d3] text-white px-2 h-fit w-fit rounded ml-6">
              Status
            </h4>
            <div className="flex items-center w-full mt-3 text-xs font-medium text-gray-400 ml-6">
              <div className="flex items-center">
                <svg
                  className="w-4 h-4 fill-current text-red-500"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                >
                  <path
                    fillRule="evenodd"
                    d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z"
                    clipRule="evenodd"
                  />
                </svg>
                <span className="leading-none text-red-500">Dec 12</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
  
  export default Plan;
  