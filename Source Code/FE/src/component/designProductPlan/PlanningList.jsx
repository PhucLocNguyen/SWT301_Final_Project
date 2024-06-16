import React, { useState } from 'react';
import Plan from './Plan';
import Popup from './Popup';

function PlanningList() {
  const [isOpenPopup, setIsOpenPopup] = useState(false);

  return (
    <>
      {/* Bắt đầu phần nav */}
      <div className="flex flex-col w-screen h-screen overflow-auto text-gray-700 bg-gradient-to-tr from-blue-200 via-indigo-200 to-pink-200">
        <div className="flex items-center flex-shrink-0 w-full h-16 px-10 bg-white bg-opacity-75">
          <svg
            className="w-8 h-8 text-indigo-600 stroke-current"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M7 21a4 4 0 01-4-4V5a2 2 0 012-2h4a2 2 0 012 2v12a4 4 0 01-4 4zm0 0h12a2 2 0 002-2v-4a2 2 0 00-2-2h-2.343M11 7.343l1.657-1.657a2 2 0 012.828 0l2.829 2.829a2 2 0 010 2.828l-8.486 8.485M7 17h.01"
            />
          </svg>
          <div className="ml-10">
            <a
              className="mx-2 text-sm font-semibold text-gray-600 hover:text-indigo-700"
              href="#"
            >
              Activity
            </a>
          </div>
          <div className="flex items-center justify-center w-8 h-8 ml-auto overflow-hidden rounded-full cursor-pointer">
            <img
              src="https://assets.codepen.io/5041378/internal/avatars/users/default.png?fit=crop&format=auto&height=512&version=1600304177&width=512"
              alt="User avatar"
            />
          </div>
        </div>
        {/* kết thúc phần nav */}

        <div className="px-10 mt-6">
          <h1 className="text-2xl font-bold">Team Project Board</h1>
        </div>

        {/* các cột để chia công việc */}
        <div className="flex flex-grow px-40 mt-4 justify-around overflow-auto">
          {/* cột To-do */}
          <div className="flex flex-col flex-shrink-0 w-[28%] ">
            <div className="flex items-center flex-shrink-0 h-10 px-2">
              <span className="block text-sm font-semibold">To-do</span>
              <span className="flex items-center justify-center w-5 h-5 ml-2 text-sm font-semibold text-indigo-500 bg-white rounded bg-opacity-30">
                {/* số lượng object bỏ ngay đây */} 6
              </span>
            </div>
            <div className="flex flex-col pb-2 overflow-auto">
              <Plan onClick={() => setIsOpenPopup(true)} />
            </div>
          </div>

          {/* cột In-Progress */}
          <div className="flex flex-col flex-shrink-0 w-[28%]">
            <div className="flex items-center flex-shrink-0 h-10 px-2">
              <span className="block text-sm font-semibold">In-Progress</span>
              <span className="flex items-center justify-center w-5 h-5 ml-2 text-sm font-semibold text-indigo-500 bg-white rounded bg-opacity-30">
                {/* số lượng object bỏ ngay đây */} 6
              </span>
            </div>
            <div className="flex flex-col pb-2 overflow-auto">
              <Plan onClick={() => setIsOpenPopup(true)} />
            </div>
          </div>

          {/* cột Done */}
          <div className="flex flex-col flex-shrink-0 w-[28%]">
            <div className="flex items-center flex-shrink-0 h-10 px-2">
              <span className="block text-sm font-semibold">Done</span>
              <span className="flex items-center justify-center w-5 h-5 ml-2 text-sm font-semibold text-indigo-500 bg-white rounded bg-opacity-30">
                {/* số lượng object bỏ ngay đây */} 6
              </span>
            </div>
            <div className="flex flex-col pb-2 overflow-auto">
              <Plan onClick={() => setIsOpenPopup(true)} />
            </div>
          </div>
        </div>
      </div>
      {isOpenPopup && <Popup setIsOpenPopup={setIsOpenPopup} />}
    </>
  );
}

export default PlanningList;
