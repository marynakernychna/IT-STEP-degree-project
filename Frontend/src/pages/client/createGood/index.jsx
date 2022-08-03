import React, { useEffect, useState } from 'react';
import { Image, Layout, Input, InputNumber, Space, Dropdown, Button, Menu, Select } from 'antd';
import { imgFallback } from '../../../constants/others';
import { TagsOutlined } from '@ant-design/icons';
import { getCategories } from '../../../services/categories';

const { TextArea } = Input;

const CreateGoodPage = () => {
    const [categories, setCategories] = useState([]);

    useEffect(async () => {
        let goodCategories = await getCategories();

        goodCategories.map(category => {
            category.value = category.title;
            category.label = category.title;
        });

        setCategories(goodCategories);
    }, []);

    const onFinishFailed = () => {

    };

    const onFinish = () => {
    };

    if (categories.count === 0) {
        return <>Loading...</>
    }

    return (
        <Layout id="createGoodPage">

            <div id="container">
                <p id="title">Create a good</p>

                <Layout id="infoBlock">
                    <img
                        src={imgFallback}
                        onClick={() => console.log("1")}
                    />

                    <Input
                        showCount
                        maxLength={100}
                    />

                    <TextArea
                        showCount
                        maxLength={1000}
                        autoSize={true}
                    />

                    <InputNumber
                        min={0}
                        max={100000}
                        addonAfter="₴ (UAH)"
                        placeholder="Cost"
                    />

                    <InputNumber
                        min={1}
                        max={5000}
                        addonAfter="item(s)"
                        placeholder="Available"
                    />

                    <Select
                        showSearch
                        placeholder="Category"
                        optionFilterProp="children"
                        filterOption={(input, option) =>
                            option.value.toLowerCase().indexOf(input.toLowerCase()) >= 0
                        }
                        options={categories}
                    >
                    </Select>
                </Layout>
            </div>

        </Layout>
    );
};

export default CreateGoodPage;
