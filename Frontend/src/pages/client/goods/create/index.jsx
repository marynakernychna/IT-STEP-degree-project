import React, { useEffect, useState } from 'react';
import { Layout, Input, InputNumber, Select, Button, Table, Form, Spin } from 'antd';
import { useHistory } from 'react-router-dom';
import { getCategories } from '../../../../services/categories';
import Upload from 'antd/lib/upload/Upload';
import ImgCrop from 'antd-img-crop';
import { UploadOutlined, CloseOutlined, PlusOutlined } from '@ant-design/icons';
import { getBase64 } from '../../../../services/helpers';
import { fileExtensions } from '../../../../constants/others';
import InputRules from '../../../../constants/inputRules';
import { generalMessages } from '../../../../constants/messages/general';
import { errorMessage, confirmMessage } from '../../../../services/alerts';
import { goodsMessages } from '../../../../constants/messages/goods';
import { createGood } from '../../../../services/goods';

const { TextArea } = Input;

const CreateGoodPage = () => {
    let history = useHistory();

    const [categories, setCategories] = useState([]);
    const [characteristics, setCharacteristics] = useState([]);
    const [characteristicName, setCharacteristicName] = useState("");
    const [characteristicValue, setCharacteristicValue] = useState("");
    const [photo, setPhoto] = useState();
    const [categoryTitle, setCategoryTitle] = useState();
    const [loading, setLoading] = useState(true);

    const characteristicsTableColumns = [
        {
            title: "Name",
            dataIndex: "name",
            maxWidth: '100px'
        },
        {
            title: "Value",
            dataIndex: "value"
        },
        {
            title: '',
            dataIndex: '',
            key: 'x',
            fixed: 'right',
            render: (_, record) =>
                <Button
                    danger
                    type="primary"
                    icon={<CloseOutlined />}
                    onClick={() => deleteCharacteristic(record)}
                >
                    Delete
                </Button>
        }
    ];

    useEffect(async () => {
        let goodCategories = await getCategories();

        goodCategories.map(category => {
            category.value = category.title;
            category.label = category.title;
        });

        setCategories(goodCategories);
        setLoading(false);
    }, []);

    const onFinishFailed = () => {
        errorMessage(
            goodsMessages.CREATE_GOOD_FAILED,
            generalMessages.CORRECT_ALL_COMMENTS
        );
    };

    const onFinish = async (values) => {
        setLoading(true);
        if (photo === undefined) {
            errorMessage(
                goodsMessages.NO_IMAGE,
                goodsMessages.CREATE_GOOD_FAILED
            );
            setLoading(false);
            return;
        }

        if (characteristics.length === 0) {
            const confirm = await confirmMessage(
                goodsMessages.NO_CHARACTERISTICS_WARNING,
                generalMessages.ARE_YOU_SURE
            )
                .then((result) => {
                    setLoading(false);
                    return result;
                });

            if (!confirm) {
                setLoading(false);
                return;
            }
        }

        const photoBase64 = photo.split(',')[1];
        const photoExtension = '.' + photo.split('/')[1].split(';')[0];

        const model = {
            title: values.name,
            description: values.description,
            cost: values.cost,
            photoBase64: photoBase64,
            photoExtension: photoExtension,
            availableCount: values.available,
            categoryTitle: categoryTitle,
            characteristics: characteristics
        };

        createGood(model, history);
        setLoading(false);
    };

    const addCharacteristic = () => {
        setLoading(true);
        if (characteristicName.trim() !== "" &&
            characteristicValue.trim() !== "") {

            let duplicate = false;

            characteristics.forEach(characteristic => {
                if (characteristic.name === characteristicName) {
                    duplicate = true;
                    setLoading(false);
                    return;
                }
            });

            if (!duplicate) {
                setCharacteristics([
                    ...characteristics,
                    {
                        name: characteristicName,
                        value: characteristicValue
                    }
                ]);
                setLoading(false);
            }
        }
    };

    const deleteCharacteristic = (record) => {
        setLoading(true);
        const elementIndex = characteristics.indexOf(record);

        setCharacteristics([
            ...characteristics.slice(0, elementIndex),
            ...characteristics.slice(elementIndex + 1, characteristics.length)
        ]);
        setLoading(false);
    };

    const beforeUpload = async (file) => {
        setLoading(true);

        if (file.type === fileExtensions.JPG ||
            file.type === fileExtensions.PNG) {

            let fullPhotoBase64String = await getBase64(file);
            setPhoto(fullPhotoBase64String);
            setLoading(false);
        }

        setLoading(false);

        return false;
    };

    if (categories.count === 0) {
        return <>Loading...</>
    }
    return (
        <Spin size="large" spinning={loading}>
            <Layout id="createGoodPage">

                <div id="container">
                    <p id="title">Create a good</p>

                    <Layout id="infoBlock">

                        <div id="upperBlock">
                            <div className="leftSide">

                                {photo !== undefined ?
                                    <img src={photo} />
                                    :
                                    <></>
                                }

                                <ImgCrop rotate>
                                    <Upload
                                        accept=".png, .jpg"
                                        action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                                        fileList={[]}
                                        showUploadList={false}
                                        beforeUpload={(file) => beforeUpload(file)}
                                    >
                                        <Button icon={<UploadOutlined />}>Click to upload a photo</Button>
                                    </Upload>
                                </ImgCrop>
                            </div>

                            <div className="rightSide">
                                {
                                    characteristics.length > 0 ?
                                        <Table
                                            bordered
                                            columns={characteristicsTableColumns}
                                            dataSource={[...characteristics]}
                                            pagination={false}
                                            size="small"
                                            id="characteristics"
                                            scroll={{
                                                x: true
                                            }}
                                        />
                                        :
                                        <></>
                                }

                                <h3>Characteristics</h3>

                                <TextArea
                                    placeholder="Name"
                                    showCount
                                    maxLength={50}
                                    autoSize={true}
                                    onChange={(e) => setCharacteristicName(e.target.value)}
                                />

                                <TextArea
                                    placeholder="Value"
                                    showCount
                                    maxLength={200}
                                    autoSize={true}
                                    onChange={(e) => setCharacteristicValue(e.target.value)}
                                />

                                <Button
                                    id="addCharacteristicButton"
                                    onClick={() => addCharacteristic()}
                                >
                                    <PlusOutlined />
                                    Add
                                </Button>
                            </div>
                        </div>

                        <Form
                            labelCol={{ span: 5 }}
                            wrapperCol={{ span: 32 }}
                            onFinish={onFinish}
                            onFinishFailed={onFinishFailed}
                            scrollToFirstError
                            id="goodForm"
                        >
                            <Form.Item
                                label="Enter the title: "
                                name="name"
                                rules={[
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    ),
                                    InputRules.notEmpty(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <TextArea
                                    showCount
                                    maxLength={100}
                                    autoSize={true}
                                    placeholder="Title"
                                />
                            </Form.Item>

                            <Form.Item
                                label="Enter the description: "
                                name="description"
                                rules={[
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    ),
                                    InputRules.notEmpty(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <TextArea
                                    showCount
                                    maxLength={1000}
                                    autoSize={true}
                                    placeholder="Description"
                                />
                            </Form.Item>

                            <Form.Item
                                label="Enter the cost: "
                                name="cost"
                                rules={[
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <InputNumber
                                    min={0}
                                    max={100000}
                                    addonAfter="₴ (UAH)"
                                    placeholder="Cost"
                                />
                            </Form.Item>

                            <Form.Item
                                label="Enter the available count: "
                                name="available"
                                rules={[
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <InputNumber
                                    min={1}
                                    max={5000}
                                    addonAfter="item(s)"
                                    placeholder="Available"
                                />
                            </Form.Item>

                            <Form.Item
                                label="Select the category: "
                                name="category"
                                rules={[
                                    InputRules.required(
                                        generalMessages.FIELD_MUST_NOT_BE_EMPTY
                                    )
                                ]}
                            >
                                <Select
                                    showSearch
                                    placeholder="Category"
                                    optionFilterProp="children"
                                    filterOption={(input, option) =>
                                        option.value.toLowerCase().indexOf(input.toLowerCase()) >= 0
                                    }
                                    options={categories}
                                    onChange={(value) => setCategoryTitle(value)}
                                >
                                </Select>
                            </Form.Item>

                            <div className="submitButtonDiv">
                                <Button
                                    className='submitButton'
                                    htmlType="submit"
                                    type="primary"
                                >
                                    Create
                                </Button>
                            </div>
                        </Form>
                    </Layout>
                </div>
            </Layout>
        </Spin>
    );
};

export default CreateGoodPage;
