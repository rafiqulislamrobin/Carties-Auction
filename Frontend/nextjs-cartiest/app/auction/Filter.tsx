import { Button, ButtonGroup } from "flowbite-react";
import React from "react";
import { useParamsStore } from "../hooks/useParamsStore";
import { AiOutlineClockCircle, AiOutlineSortAscending } from "react-icons/ai";
import { BsFillStopCircleFill } from "react-icons/bs";

const pageSizeButtons = [4, 8, 12];
const orderButtons = [
  {
    label: 'Alphabetical',
    icon: AiOutlineSortAscending,
    value: 'make'
  },
  {
    label: 'Ending soon',
    icon: AiOutlineClockCircle,
    value: 'endingSoon'
  },
  {
    label: 'Recently added',
    icon: BsFillStopCircleFill,
    value: 'new'
  }
]

export default function Filter() {
    const pageSize= useParamsStore(state => state.pageSize);
    const setParams = useParamsStore(state => state.setParams);
    const orderBy = useParamsStore(state => state.orderBy);
  return (
    <div>
      <div>
                <span className='uppercase text-sm text-gray-500 mr-2'>Filter by</span>
                <Button.Group>
                    {orderButtons.map(({ label, icon: Icon, value }) => (
                        <Button
                            key={value}
                            onClick={() => setParams({ orderBy: value })}
                            color={`${orderBy === value ? 'red' : 'gray'}`}
                        >
                            <Icon className='mr-3 h-4 w-4' />
                            {label}
                        </Button>
                    ))}
                </Button.Group>
            </div>

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


