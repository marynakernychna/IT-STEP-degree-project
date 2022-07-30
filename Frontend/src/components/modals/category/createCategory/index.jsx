import React from 'react';
import { errorMessage, successMessage } from '../../../../services/alerts';
import { Modal, Form, Input, Button } from "antd";
import { generalMessages } from '../../../../constants/messages/general';
import InputRules from '../../../../constants/inputRules';
import { inputValidationErrorMessages } from '../../../../constants/messages/inputValidationErrors';
import { createCategory } from '../../../../services/categories';

function CreateCategoryModal(props) {

    const close = () => {
        props.myClose();
    };

    const updateCategories = () => {
        props.updateCategories();
    };

    const create = async (values) => {
        var isSuccessful = await createCategory(values.title);

        if (isSuccessful) {
            successMessage(
                generalMessages.CREATION_SUCCESSFUL
            );
            updateCategories();
            close();
        }
    }

    const onFinishFailed = () => {
        errorMessage(
            generalMessages.CORRECT_ALL_COMMENTS,
            ""
        );
    };

    return (
        <Modal
            title="Create a category"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
        >
            <Form
                labelCol={{ span: 5 }}
                wrapperCol={{ span: 32 }}
                onFinish={create}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="title"
                    label="Title: "
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_CATEGORY
                        ),
                        InputRules.lengthRange(
                            2,
                            50,
                            inputValidationErrorMessages.CATEGORY_MUST_BE_BETWEEN_1_AND_50
                        )
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        htmlType="submit"
                        type="primary"
                        className="submitButton"
                    >
                        Save
                    </Button>
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        onClick={() => close()}
                    >
                        Close
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default CreateCategoryModal;
