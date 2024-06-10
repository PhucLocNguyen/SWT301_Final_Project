import react from "react";
import Logocard from "../../assets/footer/Logocard.webp";
import { SvgIcon } from "@mui/material";
import EditLocationIcon from "@mui/icons-material/EditLocation";
import CallIcon from "@mui/icons-material/Call";
import AlarmOnIcon from "@mui/icons-material/AlarmOn";
import EmailIcon from "@mui/icons-material/Email";
import FacebookIcon from "@mui/icons-material/Facebook";
import InstagramIcon from "@mui/icons-material/Instagram";
import XIcon from "@mui/icons-material/X";
import { color } from "framer-motion";
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MonetizationOnIcon from '@mui/icons-material/MonetizationOn';
import ForumIcon from '@mui/icons-material/Forum';
import WorkspacePremiumIcon from '@mui/icons-material/WorkspacePremium';

function Footer() {
  return (
    <>
      {/* Label */}
      <div className="flex justify-around items-center bg-[#fdf9f2] h-[100px] w-full">
        <div className="w-fit">
          <LocalShippingIcon style={{}}/> <span className="font-light text-xl">Free Delivery</span>
        </div>
        <div className="w-fit">
          <MonetizationOnIcon/> <span className="font-light text-xl">Money Back Guarantee</span>
        </div>
        <div className="w-fit">
          <ForumIcon/> <span className="font-light text-xl">24/7 Support</span>
        </div>
        <div className="w-fit">
          <WorkspacePremiumIcon/> <span className="font-light text-xl">High Quality</span>
        </div>
      </div>

      <footer className="h-[400px] w-full bg-black">
        {/* container */}
        <div className="flex justify-around pt-12 pb-9">
          {/* Elements */}
          <div>
            <h3 className="text-[#C6AD8A] mb-2">MY ACCOUNT</h3>
            <ul className="relative list-disc text-[#C6AD8A] mt-3">
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                My account
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Cart
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Checkout
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Maintenance Mode
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Register Now
              </li>
            </ul>
          </div>
          {/* Elements */}
          <div>
            <h3 className="text-[#C6AD8A] mb-2">Information</h3>
            <ul className="relative list-disc text-[#C6AD8A] mt-2">
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                About Us
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Our Blog
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Contact
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Term & Condition
              </li>
              <li className="ml-5 mt-6 w-fit text-sm text-[#a3a3a3] hover:text-white hover:underline duration-300">
                Refund and Returns Policy
              </li>
            </ul>
          </div>
          {/* Elements */}
          <div>
            <h3 className="text-[#C6AD8A] mb-2">Our Contacts</h3>
            <ul className="relative text-[#C6AD8A] mt-2">
              <li className=" mt-4 w-fit text-sm text-[#d6cece]">
                <EditLocationIcon
                  fontSize="small"
                  className="w-2 mr-2  text-[#C6AD8A]"
                />
                283 N. Glenwood Street, Levittown, NY 11756
              </li>
              <li className=" mt-4 w-fit text-sm text-[#d6cece]">
                <CallIcon
                  fontSize="small"
                  className="w-2 mr-2  text-[#C6AD8A]"
                />
                712-339-9294
              </li>
              <li className=" mt-4 w-fit text-sm text-[#d6cece]">
                <AlarmOnIcon
                  fontSize="small"
                  className="w-2 mr-2  text-[#C6AD8A]"
                />
                Mon - Fri: 10:00 - 18:00
              </li>
              <li className=" mt-4 w-fit text-sm text-[#d6cece]">
                <EmailIcon
                  fontSize="small"
                  className="w-2 mr-2  text-[#C6AD8A]"
                />
                info@goldish-jew.com
              </li>
            </ul>
            <div className="flex w-full mt-4">
              {/* contact logo */}
              <div className="relative mr-4 border-[#4f4e4e] border-[1px] w-12 h-12 rounded-full">
                <FacebookIcon
                  fontSize="small"
                  sx={{
                    "&:hover": {
                      color: "#C6AD8A",
                      transitionDuration: "300ms",
                    },
                  }}
                  className="absolute bottom-[13px] ml-[14px] text-white"
                />
              </div>
              <div className="relative mr-3 border-[#4f4e4e] border-[1px] w-12 h-12 rounded-full">
                <InstagramIcon
                  fontSize="small"
                  className="absolute bottom-[13px] ml-[14px] text-white hover:text-[#C6AD8A] duration-300"
                />
              </div>
              <div className="relative mr-3 border-[#4f4e4e] border-[1px] w-12 h-12 rounded-full">
                <XIcon
                  fontSize="small"
                  className="absolute bottom-[13px] ml-[14px] text-white hover:text-[#C6AD8A] duration-300"
                />
              </div>
            </div>
          </div>
        </div>
        {/* reserve */}
        <div className="flex box-border px-9 justify-between align-middle w-full border-[#2b2b2b] border-t-[1px]">
          {/* word */}
          <div>
            <h1 className="text-[#5c5757] py-5">
              2021 Goldish Theme. All rights reserved.
            </h1>
          </div>
          {/* logo */}
          <div className="w-40 py-5">
            <img className="w-fit" src={Logocard} />
          </div>
        </div>
      </footer>
    </>
  );
}

export default Footer;
