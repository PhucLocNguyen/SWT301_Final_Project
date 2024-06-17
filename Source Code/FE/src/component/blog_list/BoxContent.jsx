import { px } from "framer-motion";

const BoxContent = ({data}) => {

  console.log(data)
  return (
    <>
      <div className="flex flex-wrap justify-between mt-10 ">
        {/* blog */}
        <div className="mb-4">
          <div className="h-[175px]  overflow-hidden">
            <img
              src="https://raw.githubusercontent.com/PhucLocNguyen/N2_NET1806_SWP/d3aea75f8807950444c170170b340772eebc8bd5/FE/src/assets/blogList/bannerPicBlog1-2022.jpg"
              style={{
                transition: "transform 8s cubic-bezier(.25,.46,.45,.94)",
              }}
              className="object-cover min-w-full h-full hover:origin-center hover:scale-125 delay-[7000ms]"
            />
          </div>

          <p className="my-3 min-h-12">
            {data.title}
          </p>

          <p className="my-3 text-sm line-clamp-4 text-justify mr-3">
            {data.description}
          </p>

          <a className="underline text-sm mt-4 cursor-pointer" href={data.title}>READ MORE</a>
        </div>
      </div>
    </>
  );
};

export default BoxContent;
