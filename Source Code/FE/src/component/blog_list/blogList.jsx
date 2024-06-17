import { px } from "framer-motion";
import { React, useEffect, useState } from "react";
import bannerPic from "../../assets/blogList/bannerPic-2022.jpg";
import Pagination from "@mui/material/Pagination";
import BoxContent from "./BoxContent";
import axios from "axios";
import "./BlogList.css";

export default function blogList() {
  const [data, setData] = useState([]); 

  const headers = {
    'Authorization':
      'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InR5ZHkyMzRAZ21haWwuY29tIiwiZ2l2ZW5fbmFtZSI6InR5ZHkiLCJuYmYiOjE3MTc5MDUzNDAsImV4cCI6MTcxODUxMDE0MCwiaWF0IjoxNzE3OTA1MzQwLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjcxNjkvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo3MTY5LyJ9.wboKm8JG1nb1W1Jm8Vlkjy7x122CmGrqaTVU1m9mmSAuW1vNB50d8Mltt3lXWfc7mtrBSfup_SPf_AoXCbtt-w',
  };

  const url = "https://localhost:7286/api/Blog";

  const FetchApiBlog = async () => {
    try {
      const response = await axios.get(url,{headers});
      setData(response.data)
    } catch (error) {
      console.error(error);
      return [];
    }
  };
  useEffect(() => {
    FetchApiBlog();
  }, []);

  return (
    <>
      {/* Banner Picture */}
      <div className="relative w-full h-60 overflow-hidden">
        <div className="absolute top-0 left-0 w-full h-full bg-gradient-to-b from-transparent to-[#0000004f] z-10 opacity-0 animate-fade-in-up">
          <div className="flex flex-col justify-center items-center h-full text-center">
            <h1 className="text-xl text-white">JEWELLERY BLOG</h1>
            <p className="text-white">
              Articles by Australian designer Simone Walsh
            </p>
          </div>
        </div>
        <img
          src={bannerPic}
          className="absolute top-0 left-0 w-full h-full object-cover object-center scale-110 opacity-0 animate-zoom-in"
          style={{
            objectPosition: "51.6277% 55.0512%",
          }}
        />
      </div>

      {/* content */}
      <div className="grid grid-cols-3 gap-10 mx-96">
        {data.map((items, index) => {
          return (<BoxContent key={index} data={items}/>)
        })}
      </div>

      <div className="flex justify-center my-6">
        <Pagination count={10} shape="rounded" className="mt-10 scale-150" />
      </div>
    </>
  );
}
