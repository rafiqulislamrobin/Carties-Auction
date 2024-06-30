import { Button, ButtonGroup } from "flowbite-react";
import React from "react";
import { useParamsStore } from "../hooks/useParamsStore";

const pageSizeButtons = [4, 8, 12];
type Props = {
  pageSize: number;
  setPageSize: (size: number) => void;
};

export default function Filter() {
    const pageSize= useParamsStore(state => state.pageSize);
    const setParams = useParamsStore(state => state.setParams);
  return (
    <div>
      <span className="uppercase text-sm text-gray-500 mr-2">Page size</span>
      <Button.Group >
        {pageSizeButtons.map((value, i) => (
          <Button
            key={i}
            onClick={() => setParams({pageSize: value})}
            color={`${pageSize === value ? 'red' : 'gray'}`}
            className={` px-2 focus:ring-0 ${pageSize === value ? 'border-red-500' : 'border-gray-500'}`}
          >
            {value}
          </Button>
        ))}
      </Button.Group >
    </div>
  );
}


