import React from "react";
import { AiOutlineCar } from "react-icons/ai";
import Search from "./Search";

export default function navbar() {
  return (
    <header
      className="
    sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md
  "
    >
      <div className="flex items-center gap-2 text-3xl font-semibold text-purple-500">
        <AiOutlineCar />
        <div >
          Carties Auction
        </div>
      </div>
      <Search/>
      <div>login</div>
    </header>
  );
}
