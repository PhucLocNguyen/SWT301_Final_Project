import EditLocationIcon from "@mui/icons-material/EditLocation";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import { SvgIcon } from "@mui/material";
import PermIdentityIcon from "@mui/icons-material/PermIdentity";
import ShoppingBagIcon from "@mui/icons-material/ShoppingBag";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { Link } from "react-router-dom";

function Navbar() {
  return (
    <>
      <div className="h-[111px]">
        <header className="fixed top-0 w-full z-50">
          <nav className="flex h-20 border-b-[1px] border-black bg-white px-12 pt-[30px] pb-[5rem] justify-between">
            {/* Left header */}
            <div className="w-1/4">
              <div>
                <span className="font-normal text-sm">
                  <EditLocationIcon
                    style={{ fontSize: "1.1rem", marginRight: "0.4rem" }}
                  />
                  283 N. Glenwood Street, Levittown, NY{" "}
                </span>
              </div>
              <div className="mt-4">
                <span className="font-normal text-sm">
                  <LocalPhoneIcon
                    style={{ fontSize: "1.1rem", marginRight: "0.4rem" }}
                  />
                  0908935565{" "}
                </span>
              </div>
            </div>
            {/* middle header */}
            <div className="w-1/2">
              {/* Logo */}
              <div className="text-center">
                <SvgIcon style={{ fontSize: "9rem", height: "50%" }}>
                  <svg
                    width="140"
                    height="14"
                    viewBox="0 0 140 14"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M8.16991 8.59615H11.3139C11.1984 9.04487 11.0253 9.46795 10.7944 9.86538C10.5635 10.2628 10.2749 10.6154 9.92851 10.9231C9.58217 11.2308 9.17168 11.4679 8.69707 11.6346C8.22245 11.8013 7.68369 11.8846 7.0808 11.8846C6.1957 11.8846 5.40039 11.6795 4.69488 11.2692C3.98936 10.859 3.43136 10.2885 3.02088 9.55769C2.6104 8.8141 2.40516 7.95513 2.40516 6.98077C2.40516 5.99359 2.6104 5.14102 3.02088 4.42308C3.44419 3.69231 4.0086 3.12821 4.71412 2.73077C5.43246 2.32051 6.22135 2.11538 7.0808 2.11538C8.01721 2.11538 8.81251 2.30769 9.46672 2.69231C10.1337 3.07692 10.6533 3.58974 11.0253 4.23077L12.8724 3C12.4748 2.37179 11.9937 1.83974 11.4293 1.40385C10.8649 0.955129 10.2171 0.608975 9.48596 0.365385C8.76762 0.121795 7.9659 0 7.0808 0C6.06742 0 5.13101 0.173077 4.27157 0.519231C3.41212 0.852565 2.66171 1.33333 2.02034 1.96154C1.37896 2.57692 0.878686 3.3141 0.519515 4.17308C0.173172 5.01923 0 5.95513 0 6.98077C0 8.00641 0.173172 8.94872 0.519515 9.80769C0.865858 10.6667 1.3533 11.4103 1.98185 12.0385C2.62323 12.6667 3.37364 13.1538 4.23309 13.5C5.09253 13.8333 6.02894 14 7.04231 14C8.11983 14 9.07548 13.8077 9.90927 13.4231C10.7431 13.0256 11.4422 12.4872 12.0066 11.8077C12.571 11.1282 12.9943 10.3526 13.2765 9.48077C13.5587 8.59615 13.687 7.66026 13.6613 6.67308H8.16991V8.59615Z"
                      fill="#181818"
                    />
                    <path
                      fillRule="evenodd"
                      clipRule="evenodd"
                      d="M24.5522 6.98077C24.5522 7.98077 24.7254 8.91026 25.0717 9.76923C25.4181 10.6282 25.9055 11.3718 26.534 12C27.1626 12.6282 27.9002 13.1218 28.7468 13.4808C29.6062 13.8269 30.5362 14 31.5368 14C32.563 14 33.4994 13.8269 34.346 13.4808C35.1926 13.1218 35.9302 12.6282 36.5588 12C37.1873 11.3718 37.6748 10.6282 38.0211 9.76923C38.3674 8.91026 38.5406 7.98077 38.5406 6.98077C38.5406 5.96795 38.3674 5.03846 38.0211 4.19231C37.6748 3.34615 37.1809 2.61538 36.5395 2C35.911 1.37179 35.1734 0.884616 34.3268 0.538462C33.4802 0.179487 32.5502 0 31.5368 0C30.5491 0 29.6255 0.179487 28.766 0.538462C27.9194 0.884616 27.1754 1.37179 26.534 2C25.9055 2.61538 25.4181 3.34615 25.0717 4.19231C24.7254 5.03846 24.5522 5.96795 24.5522 6.98077ZM26.9574 6.98077C26.9574 6.05769 27.1562 5.23718 27.5538 4.51923C27.9515 3.78846 28.4902 3.21795 29.1701 2.80769C29.8628 2.39744 30.6517 2.19231 31.5368 2.19231C32.4475 2.19231 33.2428 2.39744 33.9227 2.80769C34.6154 3.21795 35.1542 3.78846 35.539 4.51923C35.9238 5.23718 36.1162 6.05769 36.1162 6.98077C36.1162 7.90385 35.9174 8.72436 35.5197 9.44231C35.1349 10.1603 34.5962 10.7308 33.9035 11.1538C33.2236 11.5641 32.4347 11.7692 31.5368 11.7692C30.6517 11.7692 29.8628 11.5641 29.1701 11.1538C28.4902 10.7308 27.9515 10.1603 27.5538 9.44231C27.1562 8.72436 26.9574 7.90385 26.9574 6.98077Z"
                      fill="#181818"
                    />
                    <path
                      d="M50.0995 13.7115V0.25H52.37V11.6731H58.788V13.7115H50.0995Z"
                      fill="#181818"
                    />
                    <path
                      fillRule="evenodd"
                      clipRule="evenodd"
                      d="M69.7641 0.25V13.7115H74.8294C76.2019 13.7115 77.4077 13.4359 78.4468 12.8846C79.4858 12.3205 80.2939 11.5385 80.8712 10.5385C81.4612 9.52564 81.7563 8.33974 81.7563 6.98077C81.7563 5.62179 81.4612 4.44231 80.8712 3.44231C80.2939 2.42949 79.4858 1.64744 78.4468 1.09615C77.4077 0.532051 76.2019 0.25 74.8294 0.25H69.7641ZM74.7717 11.5769H72.0923V2.38461H74.7717C75.4387 2.38461 76.0544 2.48077 76.6188 2.67308C77.1833 2.86539 77.6707 3.15385 78.0812 3.53846C78.5045 3.92308 78.8316 4.40385 79.0625 4.98077C79.3062 5.55769 79.4281 6.22436 79.4281 6.98077C79.4281 7.73718 79.3062 8.40384 79.0625 8.98077C78.8316 9.55769 78.5045 10.0385 78.0812 10.4231C77.6707 10.7949 77.1833 11.0833 76.6188 11.2885C76.0544 11.4808 75.4387 11.5769 74.7717 11.5769Z"
                      fill="#181818"
                    />
                    <path
                      d="M93.5805 13.7115V0.25H95.8895V13.7115H93.5805Z"
                      fill="#181818"
                    />
                    <path
                      d="M109.097 9.36538L107.403 10.5577C107.698 11.1731 108.096 11.7436 108.596 12.2692C109.109 12.7949 109.699 13.2179 110.366 13.5385C111.046 13.8462 111.777 14 112.56 14C113.137 14 113.689 13.9038 114.215 13.7115C114.753 13.5321 115.235 13.2756 115.658 12.9423C116.081 12.5962 116.415 12.1731 116.658 11.6731C116.915 11.1731 117.043 10.609 117.043 9.98077C117.043 9.39102 116.941 8.8782 116.735 8.44231C116.53 7.99359 116.261 7.60897 115.927 7.28846C115.607 6.96795 115.241 6.69872 114.83 6.48077C114.433 6.26282 114.029 6.08333 113.618 5.94231C112.9 5.6859 112.316 5.4359 111.867 5.19231C111.418 4.94872 111.085 4.69231 110.867 4.42308C110.661 4.14103 110.559 3.82692 110.559 3.48077C110.559 3.09615 110.706 2.76282 111.001 2.48077C111.296 2.1859 111.758 2.03846 112.387 2.03846C112.836 2.03846 113.227 2.12821 113.561 2.30769C113.907 2.47436 114.208 2.70513 114.465 3C114.721 3.28205 114.939 3.58974 115.119 3.92308L116.947 2.88462C116.729 2.41026 116.415 1.95513 116.004 1.51923C115.594 1.08333 115.093 0.724359 114.503 0.442308C113.913 0.147436 113.221 0 112.425 0C111.617 0 110.892 0.153846 110.251 0.461539C109.622 0.769231 109.122 1.19872 108.75 1.75C108.378 2.28846 108.192 2.91667 108.192 3.63461C108.192 4.26282 108.314 4.80128 108.558 5.25C108.801 5.6859 109.109 6.05769 109.481 6.36538C109.853 6.66026 110.245 6.91026 110.655 7.11538C111.066 7.30769 111.438 7.46154 111.771 7.57692C112.335 7.78205 112.836 7.99359 113.272 8.21154C113.708 8.41667 114.042 8.67307 114.272 8.98077C114.516 9.27564 114.638 9.66667 114.638 10.1538C114.638 10.6923 114.446 11.1282 114.061 11.4615C113.676 11.7949 113.176 11.9615 112.56 11.9615C112.06 11.9615 111.598 11.8526 111.175 11.6346C110.764 11.4167 110.386 11.1154 110.039 10.7308C109.706 10.3462 109.392 9.89102 109.097 9.36538Z"
                      fill="#181818"
                    />
                    <path
                      d="M130.957 5.44231V0.25H128.686V13.7115H130.957V7.53846H137.73V13.7115H140V0.25H137.73V5.44231H130.957Z"
                      fill="#181818"
                    />
                  </svg>
                </SvgIcon>
              </div>
              {/* Navbar */}
              <div className="flex justify-between px-36 mt-4">
                <div className="relative group">
                  <Link
                    to="/"
                    className="font-normal text-sm hover:text-[#C6AD8A] hover:duration-200"
                  >
                    Home
                    <KeyboardArrowUpIcon
                      style={{
                        transition: "transform 0.4s ease",
                      }}
                      className="group-hover:transform group-hover:rotate-180"
                    />
                  </Link>
                  <div className="absolute top-4 left-4 h-6 w-10"></div>
                  <div className="absolute top-10 group-hover:block hidden w-[200px] h-[200px] bg-white border-[1px] border-black"></div>
                </div>
                <div className="group">
                  <Link
                    to="/design"
                    className="font-normal text-sm hover:text-[#C6AD8A] hover:duration-200"
                  >
                    Shop
                    <KeyboardArrowUpIcon
                      style={{
                        transition: "transform 0.4s ease",
                      }}
                      className="group-hover:transform group-hover:rotate-180"
                    />
                  </Link>
                </div>
                <div className="group">

                  <Link to='/blog' className="font-normal text-sm hover:text-[#C6AD8A] hover:duration-200">
                    Blog
                    <KeyboardArrowUpIcon
                      style={{
                        transition: "transform 0.4s ease",
                      }}
                      className="group-hover:transform group-hover:rotate-180"
                    />
                  </Link>
                </div>
                <div className="group">
                  <Link
                    to="#"
                    className="font-normal text-sm hover:text-[#C6AD8A] hover:duration-200"
                  >
                    About Us
                    <KeyboardArrowUpIcon
                      style={{
                        transition: "transform 0.4s ease",
                      }}
                      className="group-hover:transform group-hover:rotate-180"
                    />
                  </Link>
                </div>
              </div>
            </div>
            {/* right header */}
            <div className="w-1/4 h-full">
              {/* Icon navbar */}
              <div className="flex text-right items-center align-middle">
                {/* <SearchIcon style={{ marginLeft: "1rem", fontSize: "1.4rem" }} /> */}
                <form className="flex w-full">
                  <label
                    htmlFor="default-search"
                    className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white"
                  >
                    Search
                  </label>
                  <div className="relative w-full">
                    <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                      <svg
                        className="w-4 h-4 text-gray-500 dark:text-gray-400"
                        aria-hidden="true"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 20 20"
                      >
                        <path
                          stroke="currentColor"
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          strokeWidth="2"
                          d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                        />
                      </svg>
                    </div>
                    <input
                      type="search"
                      id="default-search"
                      className="block w-3/4 px-2 py-1 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-[#f0cd8c] dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-[#f0cd8c] dark:focus:border-[#f0cd8c]"
                      placeholder="Search..."
                      required
                    />
                    <button
                      type="submit"
                      className="absolute top-[1px] right-[-4px] text-white bg-[#494949] hover:bg-slate-900  focus:ring-4 focus:outline-none focus:ring-[#f0cd8c] font-medium rounded-lg text-sm px-[10px] py-1 dark:bg-[#f0cd8c] dark:hover:bg-[#f0cd8c] dark:focus:ring-[#f0cd8c]"
                    >
                      Search
                    </button>
                  </div>
                </form>

                <div className="relative group inline-block">
                  <div className="absolute rounded-sm top-8 left-[-1rem] group-hover:block hidden w-fit h-fit bg-white border-[1px] border-[#f0f0f0]">
                    <div className="absolute top-[-10px] left-8 h-5 w-7"></div>
                    <ul className="flex flex-col">
                      <li className="w-36">
                        <a
                          href=""
                          className="block box-border text-left  hover:text-[#f0cd8c] hover:bg-[#706f6e] p-2 duration-100"
                        >
                          Tài khoản của tôi
                        </a>
                      </li>
                      <li className="w-36">
                        <a
                          href=""
                          className="block box-border text-left  hover:text-[#f0cd8c] hover:bg-[#706f6e] p-2 duration-100"
                        >
                          Đơn mua
                        </a>
                      </li>
                      <li className="w-36">
                        <a
                          href=""
                          className="block box-border text-left  hover:text-[#f0cd8c] hover:bg-[#706f6e] p-2 duration-100"
                        >
                          Đăng xuất
                        </a>
                      </li>
                    </ul>
                  </div>
                  <PermIdentityIcon
                    style={{ marginLeft: "1rem", fontSize: "1.4rem" }}
                    className="hover:text-[#f0cd8c]"
                  />
                </div>

                <FavoriteBorderIcon
                  style={{
                    marginLeft: "1rem",
                    marginTop: "3px",
                    fontSize: "1.4rem",
                  }}
                  className="hover:text-[#f0cd8c]"
                />
                <ShoppingBagIcon
                  style={{
                    marginLeft: "1rem",
                    marginTop: "3px",
                    fontSize: "1.4rem",
                  }}
                  className="hover:text-[#f0cd8c]"
                />
              </div>
            </div>
          </nav>
        </header>
      </div>
    </>
  );
}

export default Navbar;
