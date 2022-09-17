import React, { useState } from 'react';
import { Statistic, Card, Tooltip } from 'antd';
import { DeleteOutlined, InboxOutlined } from '@ant-design/icons';
import { DEFAULT_ACTION_ICON_SIZE, DEFAULT_MOUSE_ENTER_DELAY } from './../../../../../constants/others';
import { AiOutlineEdit } from 'react-icons/ai';
import UpdateCategoryModal from '../../../../../components/modals/category/updateCategoryInfo/index';
import { confirmMessage, successMessage } from './../../../../../services/alerts';
import { deleteCategory } from './../../../../../services/categories';
import { generalMessages } from './../../../../../constants/messages/general';

const Category = (props) => {

    const [isModalOpen, setIsModalOpen] = useState(false);
    const info = props.info;

    const updateCategoryInfo = () => {
        props.updateCategoryInfo();
    };

    const onDelete = async () => {
        var result = await confirmMessage();
        
        if (result) {
            result = await deleteCategory(info.title);
            
            if (result) {
                successMessage(
                    generalMessages.DELETE_DATA_SUCCESSFULLY
                );
                updateCategoryInfo();
            }
        }
    };

    return (

        <Card className='categoryCard'>
            <div className="categoryInformation">
                <p>{info.title}</p>

                <Statistic
                    title="Total goods"
                    value={info.goodsTotalCount}
                    prefix={<InboxOutlined />}
                />

                <Statistic
                    title="Available total count"
                    value={info.availableTotalCount}
                    prefix={<InboxOutlined />}
                />

                <div className='actions'>
                    <Tooltip
                        color="#224957"
                        title="Edit"
                        placement="bottomRight"
                        mouseEnterDelay={DEFAULT_MOUSE_ENTER_DELAY}
                    >
                        <AiOutlineEdit
                            size={DEFAULT_ACTION_ICON_SIZE}
                            onClick={() => setIsModalOpen(true)}
                        />
                    </Tooltip>

                    <Tooltip
                        color="#224957"
                        title="Delete"
                        placement="bottomRight"
                        mouseEnterDelay={DEFAULT_MOUSE_ENTER_DELAY}
                    >
                        <DeleteOutlined
                            className="icon"
                            onClick={() => onDelete()}
                        />
                    </Tooltip>
                </div>
            </div>

            {
                isModalOpen &&
                <UpdateCategoryModal
                    myClose={() => setIsModalOpen(false)}
                    title={props.info.title}
                    updateCategoryInfo={() => props.updateCategoryInfo()}
                />
            }
        </Card >
    );
};

export default Category;
