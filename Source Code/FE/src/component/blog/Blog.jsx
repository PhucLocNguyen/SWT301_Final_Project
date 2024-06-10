import react from "react";
import picture from "../../assets/blog/PictureBlog1.webp";
import ShareIcon from "@mui/icons-material/Share";
import EmailIcon from "@mui/icons-material/Email";
import FacebookIcon from "@mui/icons-material/Facebook";
import InstagramIcon from "@mui/icons-material/Instagram";
import TwitterIcon from "@mui/icons-material/Twitter";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import Avatar from "../../assets/blog/Avatar.png";
import { color } from "framer-motion";
import SearchIcon from "@mui/icons-material/Search";
import blogPicture from "../../assets/blog/BlogPictue.webp";
import BlogPicture2 from "../../assets/blog/BlogPicture2.webp";

function Blog() {
  return (
    <>
      {/* Container */}
      <div className="w-full h-3/4 bg-[#fbfbfb] my-10">
        <div className="flex items-center justify-center border-b-[1px] border-[#e1e1e1] w-full h-[130px]">
          <h1 className="text-3xl font-light">
            We Working On New Luxury Collection
          </h1>
        </div>
        {/* content */}
        <div className="grid grid-cols-12 place-items-center px-[250px] mt-12">
          {/* content Blog */}
          <div className="col-span-9">
            {/* Description blog */}
            <div className="relative shadow-xl pb-14">
              <img className="w-full" src={picture} />
              <ul className="list-disc inline-flex text-[#C6AD8A] mt-5 text-xs px-6">
                <li className="list-none">
                  <span className="text-[#6A6A6A] text-sm">
                    by{" "}
                    <span className="hover:text-[#C6AD8A] duration-300">
                      GOLDISH
                    </span>
                  </span>
                </li>
                <li className=" ml-9">
                  <span className="text-[#6A6A6A] text-sm">
                    SEPTEMBER 14, 2021
                  </span>
                </li>
                <li className=" ml-9">
                  <span className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    NEW COLLECTION
                  </span>
                </li>
                <li className=" ml-9">
                  <span className="text-[#6A6A6A] text-sm">COMMENTS :3</span>
                </li>
              </ul>
              <p className="px-6 text-[#9d9d9d] mt-6">
                Consequat ornare donec non odio pellentesque. Lectus lorem
                pharetra, in quam ultrices. Vitae tortor, vestibulum mi cursus.
                <br />
                <br />
                Elementum viverra odio vel sit tellus. Nulla eget et hac morbi.
                Mauris, sem sed vitae viverra consequat maecenas bibendum elit.
                Vulputate ac fringilla in id. Nibh egestas urna, cras luctus
                eget. In rhoncus vulputate odio hac euismod ultrices gravida
                euismod. Cursus enim egestas convallis donec. Nisi, turpis arcu
                quis feugiat viverra nec.
                <br />
                <br />
                Gravida egestas enim cras ultricies ut. Ut porta placerat
                facilisis metus gravida. Dictum aliquam elit tincidunt tellus
                habitant. Pellentesque amet elementum in lectus augue vitae. Id
                morbi risus nunc sed vel id vestibulum. Amet sapien pellentesque
                eu id nunc quis id. Malesuada morbi elementum, blandit non
                quisque sed at nullam faucibus. Eget pretium blandit ornare
                ligula suscipit nullam duis tortor vitae. Urna pellentesque
                accumsan, eget scelerisque nibh amet fermentum.
                <br />
                <br />
                Amet, metus imperdiet tortor porta arcu laoreet. Interdum
                posuere ullamcorper elit urna, amet. Aliquet rhoncus quam
                vestibulum nam. Quis eu ipsum id enim mi. Sollicitudin bibendum
                tristique mauris mattis. Adipiscing dignissim ullamcorper ligula
                sodales velit, magna orci pellentesque. Tempor pellentesque
                magna dignissim vitae enim aliquam diam. Sed hac semper mattis
                egestas id potenti ut dictum.
                <br />
                <br />
                Nisl sed faucibus quam sollicitudin. Vitae, id tortor tincidunt
                at sed vitae ut sed lobortis. Ut ullamcorper nec gravida ornare
                mattis nibh eu. A elit sem donec diam in gravida duis varius.
                Facilisis nam viverra libero massa, ornare. Imperdiet urna
                quisque urna amet, enim eget nisl scelerisque. Sed posuere
                viverra purus duis suscipit ut convallis. Imperdiet et bibendum
                diam nunc, sagittis, risus turpis fusce. Vivamus quis elit
                tempor adipiscing. Tempor risus viverra arcu, lacus pharetra.
              </p>
              <div className="flex justify-between mt-9">
                <ul className="list-disc inline-flex text-[#C6AD8A] text-xs px-6">
                  <li className=" list-none">
                    <span className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                      Luxury
                    </span>
                  </li>
                  <li className="ml-6">
                    <span className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                      Working
                    </span>
                  </li>
                </ul>
                
              </div>
            </div>

            {/* NextPage */}
            <div className="relative flex justify-between items-center col-span-8 w-full h-32 mx-auto border-[1px] border-[#C6AD8A] px-4 my-14">
              {/* left side */}
              <div className="w-1/2 text-wrap">
                <ArrowBackIosNewIcon
                  style={{
                    display: "inline-block",
                  }}
                  className="text-[#C6AD8A]"
                />
                <a className="absolute top-[3rem] text-lg font-light hover:text-[#C6AD8A] duration-200 inline-block">
                  {" "}
                  New Trends In Jewelry Industry In 2021{" "}
                </a>
              </div>
              <div className="w-1/2 text-wrap text-right">
                {/* right side */}
                <a className="absolute bottom-12 right-2 text-lg font-light hover:text-[#C6AD8A] duration-200">
                  {" "}
                  Presentation New Collection{" "}
                  <ArrowForwardIosIcon className="text-[#C6AD8A] pb-1" />
                </a>
              </div>
            </div>

            {/* author comments */}
            <div className="flex">
              <img src={Avatar} className="w-fit mb-20 mr-4" />
              {/* content author */}
              <div className="ml-5">
                <p className="text-[#9d9d9d] text-xs">AUTHOR</p>
                <p className="text-black font-thin text-2xl leading-4 my-6 mb-4">
                  Goldish
                </p>
                <div className="text-[#9d9d9d]">
                  <EmailIcon
                    id="durationIcon"
                    fontSize="small"
                    className="mx-1 hover:text-black hover:duration-300"
                  />
                  <FacebookIcon
                    id="durationIcon"
                    fontSize="small"
                    className="mx-1 ml-3 hover:text-black"
                  />
                  <TwitterIcon
                    id="durationIcon"
                    fontSize="small"
                    className="mx-1 ml-3 hover:text-black"
                  />
                  <InstagramIcon
                    id="durationIcon"
                    fontSize="small"
                    className="mx-1 ml-3 hover:text-black"
                  />
                </div>
                <p className="text-[#9d9d9d] text-base mt-5">
                  For county now sister engage had season better had waited.
                  Occasional mrs interested far expression. Engage had season
                  better had waited. Occasional mrs interested far expression.
                </p>
              </div>
            </div>
          </div>


          {/* Navblog and search */}
          <div className="col-span-3 w-full h-full ml-7">
            {/* Search form */}
            <form class="border-b border-t border-[#d5d4d4] py-8 w-full mx-auto">
              <label
                for="default-search"
                class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white"
              >
                Search
              </label>
              <div class="relative">
                <div class="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                  <svg
                    class="w-4 h-4 text-gray-500 dark:text-gray-400"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 20 20"
                  >
                    <path
                      stroke="currentColor"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="2"
                      d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                    />
                  </svg>
                </div>
                <input
                  type="search"
                  id="default-search"
                  class="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-black focus:border-black dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-black dark:focus:border-black"
                  placeholder="...."
                  required
                />
                <button
                  type="submit"
                  class="text-white absolute end-2.5 bottom-2.5 bg-black hover:bg-black focus:ring-4 focus:outline-none focus:ring-black font-medium rounded-lg text-sm px-4 py-2 dark:bg-black dark:hover:bg-black dark:focus:ring-black"
                >
                  Search
                </button>
              </div>
            </form>
            {/*End Search form */}

            {/* Lasted Post */}
            <div className="mt-4">
              <h2 className="text-[#C6AD8A] text-3xl font-light">
                Lasted Posts
              </h2>
              {/* Blog */}
              <div className="mt-8">
                <div className="flex mt-8">
                  <span className="w-[45px] h-[45px]">
                    <img src={blogPicture} className="object-contain"/>
                  </span>
                  <div className="ml-4">
                    <a className="hover:text-[#C6AD8A] text-base text-[#6a6a6a] duration-300 cursor-pointer">
                      Gallery Post
                    </a>
                    <p className="text-[#6a6a6a] mt-3 text-sm">
                      SEPTEMBER 14, 2021
                    </p>
                  </div>
                </div>

                <div className="flex mt-8">
                  <span className="w-[45px] h-[45px]">
                    <img src={BlogPicture2} className=" w-[56px] object-contain"/>
                  </span>
                  <div className="ml-4">
                    <a className="hover:text-[#C6AD8A] text-base text-[#6a6a6a] duration-300 cursor-pointer">
                      Video presentation new collection
                    </a>
                    <p className="text-[#6a6a6a] mt-3 text-sm">
                      SEPTEMBER 14, 2021
                    </p>
                  </div>
                </div>
              </div>
            </div>

            {/* Tags */}
            <div className="relative mt-20 border-t border-[#d5d4d4]">
              <h2 className="text-[#C6AD8A] text-3xl font-light mt-10">Tags</h2>

              <ul className="absolute left-0 list-disc inline-flex text-[#C6AD8A] mt-5 text-xs flex-wrap">
                <li className="list-none">
                  <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    2021
                  </a>
                </li>
                <li className=" ml-5">
                <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Discounts
                  </a>
                </li>
                <li className=" ml-5">
                  <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Follow Us
                  </a>
                </li>
                <li className=" ml-5">
                  <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Gallery
                  </a>
                </li>
                <li className=" ml-5">
                <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Instagram
                  </a>
                </li>
                <li className=" ml-5">
                  <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Jewellry
                  </a>
                </li>
                <li className=" ml-5">
                <a className="text-[#6A6A6A] text-sm hover:text-[#C6AD8A] duration-300">
                    Luxury
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Blog;
