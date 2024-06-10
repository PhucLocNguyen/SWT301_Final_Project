import CategoryItem from "./CategoryItem"

function ListRing() {
   return (
      <div className="px-[6.25rem]">
         <div className="text-[1rem] leading-[1.3em] font-normal">
            <div className="grid gap-x-[2.5rem] gap-y-[2.5rem] grid-cols-4">
               <CategoryItem></CategoryItem>
               <CategoryItem></CategoryItem>
               <CategoryItem></CategoryItem>
               <CategoryItem></CategoryItem>
               <CategoryItem></CategoryItem>

            </div>
         </div>
      </div>
   )
}

export default ListRing